using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using TaskTimeManage.Core.User;

namespace SverigesForenadeFilmstudios.Repository
{
    public class TTMDbContext : DbContext
    {
        public DbSet<UserEntity> User
        {
            get; set;
        }

       

        public TTMDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            _ = optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
        }
    }
}