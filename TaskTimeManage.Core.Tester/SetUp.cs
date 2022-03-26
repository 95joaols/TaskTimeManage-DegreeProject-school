using TaskTimeManage.Domain.Context;

using TestSupport.EfHelpers;

namespace TaskTimeManage.Core
{
    class SetUp
    {
        internal TTMDbContext SetUpContext()
        {
            //var option = this.CreatePostgreSqlUniqueClassOptions<TTMDbContext>();
            var option = SqliteInMemory.CreateOptions<TTMDbContext>();
            var context = new TTMDbContext(option);
            context.Database.EnsureCreated();
            return context;
        }
    }
}