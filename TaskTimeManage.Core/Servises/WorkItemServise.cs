
using TaskTimeManage.Domain.Context;
using TaskTimeManage.Domain.Entity;

namespace TaskTimeManage.Core.Servises
{
    public class WorkItemServise
    {
        private readonly TTMDbContext context;

        public WorkItemServise(TTMDbContext context)
        {
            this.context = context;
        }

        public async Task<WorkItem> CreateTaskAsync(string name, User user)
        {
            WorkItem task = new(name, user);
            _ = await context.Task.AddAsync(task);
            _ = await context.SaveChangesAsync();

            return task;
        }
    }
}
