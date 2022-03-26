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



        public TTMDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>()
                .HasIndex(u => u.UserName)
                .IsUnique();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            _ = optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
        }
    }
}