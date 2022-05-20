using Domain.Aggregates.UserAggregate;
using Domain.Aggregates.WorkAggregate;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Application.Common.Interfaces;
public interface IApplicationDbContextWithTransaction
{
  DbSet<UserProfile> UserProfile { get; }

  DbSet<WorkItem> WorkItem { get; }

  DbSet<WorkTime> WorkTime { get; }

  Task<int> SaveChangesAsync(CancellationToken cancellationToken);
  Task<IDbContextTransaction> CreateTransactionAsync(CancellationToken cancellationToken);
}
