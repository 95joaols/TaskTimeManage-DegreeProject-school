using Application.Common.Interfaces;
using Application.moq.Configurations;
using Application.moq.Configurations.Identity;

using Domain.Aggregates.UserAggregate;
using Domain.Aggregates.WorkAggregate;
using Domain.Common;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.moq;

public class ApplicationDbContextMoq : IdentityDbContext, IApplicationDbContext
{
  public DbSet<UserProfile> UserProfile
  {
    get; set;
  }
  public DbSet<WorkItem> WorkItem
  {
    get; set;
  }
  public DbSet<WorkTime> WorkTime
  {
    get; set;
  }



  public ApplicationDbContextMoq(DbContextOptions options) : base(options)
  {
  }

  protected override void OnModelCreating(ModelBuilder builder)
  {

    builder.ApplyConfiguration(new UserProfileConfig());
    builder.ApplyConfiguration(new WorkItemConfig());
    builder.ApplyConfiguration(new WorkTimeConfig());

    builder.ApplyConfiguration(new IdentityUserLoginConfig());
    builder.ApplyConfiguration(new IdentityUserRoleConfig());
    builder.ApplyConfiguration(new IdentityUserTokenConfig());

  }

  public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken)
  {
    foreach (Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry? entry in ChangeTracker.Entries().Where(x => x.Entity is BaseAggregate))
    {
      if (entry.State == EntityState.Added)
      {
        entry.Property("PublicId").CurrentValue = Guid.NewGuid();
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

  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => _ = optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
}
