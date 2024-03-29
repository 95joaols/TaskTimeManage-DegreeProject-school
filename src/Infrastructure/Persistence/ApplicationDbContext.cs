﻿using Application.Common.Interfaces;
using Domain.Common;
using Infrastructure.Persistence.Configurations;
using Infrastructure.Persistence.Configurations.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Persistence;

public class ApplicationDbContext : IdentityDbContext, IApplicationDbContext, IApplicationDbContextWithTransaction
{
  public ApplicationDbContext(DbContextOptions options) : base(options) {}

  public DbSet<UserProfile> UserProfile{ get; set; }

  public DbSet<WorkItem> WorkItem{ get; set; }

  public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken)
  {
    foreach (var entry in ChangeTracker.Entries().Where(x => x.Entity is BaseAggregate))
    {
      if (entry.State == EntityState.Added)
      {
        entry.Property("CreatedAt").CurrentValue = DateTimeOffset.UtcNow;
        entry.Property("UpdatedAt").CurrentValue = DateTimeOffset.UtcNow;
      }
      else if (entry.State == EntityState.Modified)
      {
        entry.Property("UpdatedAt").CurrentValue = DateTimeOffset.UtcNow;
      }
    }

    int result = await base.SaveChangesAsync(cancellationToken);

    return result;
  }

  public async Task<IDbContextTransaction> CreateTransactionAsync(CancellationToken cancellationToken) =>
    await Database.BeginTransactionAsync(cancellationToken);

  protected override void OnModelCreating(ModelBuilder builder)
  {
    // Add the Postgres Extension for UUID generation
    builder.HasPostgresExtension("uuid-ossp");

    builder.ApplyConfiguration(new UserProfileConfig());
    builder.ApplyConfiguration(new WorkItemConfig());

    builder.ApplyConfiguration(new IdentityUserLoginConfig());
    builder.ApplyConfiguration(new IdentityUserRoleConfig());
    builder.ApplyConfiguration(new IdentityUserTokenConfig());
  }

  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
    optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
}