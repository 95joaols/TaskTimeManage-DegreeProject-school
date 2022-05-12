using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using TaskTimeManage.Core.Models;

namespace TaskTimeManage.Core.DataAccess;

public class TTMDataAccess : DbContext
{
	public DbSet<UserModel> User
	{
		get; set;
	}
	public DbSet<WorkItemModel> WorkItem
	{
		get; set;
	}
	public DbSet<WorkTimeModel> WorkTime
	{
		get; set;
	}



	public TTMDataAccess(DbContextOptions options) : base(options)
	{
	}

	protected override void OnModelCreating(ModelBuilder builder)
	{
		// Addd the Postgres Extension for UUID generation
		_ = builder.HasPostgresExtension("uuid-ossp");

		_ = builder.Entity<UserModel>()
				.HasIndex(u => u.UserName)
				.IsUnique();

		_ = builder.Entity<WorkItemModel>().Property(x => x.PublicId).HasDefaultValueSql("uuid_generate_v4()");
		_ = builder.Entity<WorkItemModel>().HasIndex(x => x.PublicId).IsUnique();

		_ = builder.Entity<UserModel>().Property(x => x.PublicId).HasDefaultValueSql("uuid_generate_v4()");
		_ = builder.Entity<UserModel>().HasIndex(x => x.PublicId).IsUnique();

		_ = builder.Entity<WorkTimeModel>().Property(x => x.PublicId).HasDefaultValueSql("uuid_generate_v4()");
		_ = builder.Entity<WorkTimeModel>().HasIndex(x => x.PublicId).IsUnique();



	}

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => _ = optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
}
