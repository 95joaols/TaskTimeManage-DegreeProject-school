using TaskTimeManage.Domain.Context;
using TaskTimeManage.Domain.Entity;

namespace TaskTimeManage.Core.Servises
{
    public class UserServise
    {
        private readonly TTMDbContext context;
        public UserServise(TTMDbContext context)
        {
            this.context = context;
        }
        public async Task<bool> CreateUserAsync(User user)
        {
            try
            {
                await context.User.AddAsync(user);
                int count = await context.SaveChangesAsync();
                return count > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
