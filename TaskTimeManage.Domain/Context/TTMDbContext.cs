using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using TaskTimeManage.Domain.Entity;

namespace TaskTimeManage.Domain.Context
{
	public class TTMDbContext : DbContext
	{
		public DbSet<User> User
		{
			get; set;
		}
		public DbSet<WorkItem> Task
		{
			get; set;
		}



		public TTMDbContext(DbContextOptions options) : base(options)
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
			_ = builder.Entity<User>().Property(x => x.PublicId).HasDefaultValueSql("uuid_generate_v4()");

		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => _ = optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
	}
}