using Domain.Aggregates.UserAggregate;
using Domain.Aggregates.WorkAggregate;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces;

public interface IApplicationDbContext : IDisposable
{
  DbSet<UserProfile> UserProfile{ get; }

  DbSet<WorkItem> WorkItem{ get; }

  DbSet<WorkTime> WorkTime{ get; }

  Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}