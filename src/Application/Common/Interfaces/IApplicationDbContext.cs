
using Domain.Aggregates.UserAggregate;
using Domain.Aggregates.WorkAggregate;

using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces;
public interface IApplicationDbContext : IDisposable
{
  DbSet<UserProfile> UserProfile
  {
    get;
    set;
  }
  DbSet<WorkItem> WorkItem
  {
    get;
    set;
  }
  DbSet<WorkTime> WorkTime
  {
    get;
    set;
  }
  Task<int> SaveChangesAsync(CancellationToken cancellationToken);

}
