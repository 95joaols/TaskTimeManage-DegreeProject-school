
using TaskTimeManage.Domain.Context;
using TaskTimeManage.Domain.Entity;

namespace TaskTimeManage.Core.Servises
{
    public class WorkItemServise
    {
        private TTMDbContext context;

        public WorkItemServise(TTMDbContext context)
        {
            this.context = context;
        }

        public async Task<Domain.Entity.WorkItem> CreateTaskAsync(string name, User user)
        {
            Domain.Entity.WorkItem task = new(name, user);
            await context.Task.AddAsync(task);
            await context.SaveChangesAsync();

            return task;
        }
    }
}
