
using TaskTimeManage.Domain.Context;
using TaskTimeManage.Domain.Entity;

namespace TaskTimeManage.Core.Servises
{
    public class TaskServise
    {
        private TTMDbContext context;

        public TaskServise(TTMDbContext context)
        {
            this.context = context;
        }

        public async Task<Domain.Entity.Task> CreateTaskAsync(string name, User user)
        {
            Domain.Entity.Task task = new(name, user);
            await context.Task.AddAsync(task);
            await context.SaveChangesAsync();

            return task;
        }
    }
}
