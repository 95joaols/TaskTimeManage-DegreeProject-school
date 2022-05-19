using Domain.Aggregates.UserAggregate;
using Domain.Aggregates.WorkAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces;
public interface IApplicationDbContextWithTransaction
{
  DbSet<UserProfile> UserProfile { get; }

  DbSet<WorkItem> WorkItem { get; }

  DbSet<WorkTime> WorkTime { get; }

  Task<int> SaveChangesAsync(CancellationToken cancellationToken);

  DatabaseFacade Database { get; }
}
