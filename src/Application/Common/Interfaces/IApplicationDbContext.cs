using Domain.Entities;

using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces;
public interface IApplicationDbContext : IDisposable
{
	DbSet<User> User
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