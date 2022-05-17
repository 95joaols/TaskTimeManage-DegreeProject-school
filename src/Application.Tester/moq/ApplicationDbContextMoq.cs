using Application.Common.Interfaces;

using Domain.Common;
using Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.moq;

public class ApplicationDbContextMoq : DbContext, IApplicationDbContext
{
  public DbSet<User> User
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

    _ = builder.Entity<User>()
        .HasIndex(u => u.UserName)
        .IsUnique();

    _ = builder.Entity<WorkItem>().HasIndex(x => x.PublicId).IsUnique();

    _ = builder.Entity<User>().HasIndex(x => x.PublicId).IsUnique();

    _ = builder.Entity<WorkTime>().HasIndex(x => x.PublicId).IsUnique();

  }

  public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken)
  {
    foreach (Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry? entry in ChangeTracker.Entries().Where(x => x.Entity is BaseEntity<int> or BaseEntity<string> or BaseEntity<Guid>))
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
