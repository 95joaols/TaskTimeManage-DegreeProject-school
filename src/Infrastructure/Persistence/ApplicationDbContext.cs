﻿using Application.Common.Interfaces;

using Domain.Common;
using Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.DataAccess;

public class ApplicationDbContext : DbContext, IApplicationDbContext
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



	public ApplicationDbContext(DbContextOptions options) : base(options)
	{
	}

	protected override void OnModelCreating(ModelBuilder builder)
	{
		// Addd the Postgres Extension for UUID generation
		_ = builder.HasPostgresExtension("uuid-ossp");

		_ = builder.Entity<User>()
				.HasIndex(u => u.UserName)
				.IsUnique();

		_ = builder.Entity<WorkItem>().Property(x => x.PublicId).HasDefaultValueSql("uuid_generate_v4()");
		_ = builder.Entity<WorkItem>().HasIndex(x => x.PublicId).IsUnique();

		_ = builder.Entity<User>().Property(x => x.PublicId).HasDefaultValueSql("uuid_generate_v4()");
		_ = builder.Entity<User>().HasIndex(x => x.PublicId).IsUnique();

		_ = builder.Entity<WorkTime>().Property(x => x.PublicId).HasDefaultValueSql("uuid_generate_v4()");
		_ = builder.Entity<WorkTime>().HasIndex(x => x.PublicId).IsUnique();



	}

	public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
	{
		foreach (var entry in ChangeTracker.Entries().Where(x => x.Entity is BaseEntity<int> or BaseEntity<string> or BaseEntity<Guid>))
		{
			switch (entry.State)
			{
				case EntityState.Added:
					entry.Property("CreatedAt").CurrentValue = DateTimeOffset.UtcNow;
					break;
				case EntityState.Modified:
					entry.Property("UpdatedAt").CurrentValue = DateTimeOffset.UtcNow;
					break;
			}
		}

		var result = await base.SaveChangesAsync(cancellationToken);

		return result;
	}

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => _ = optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
}
