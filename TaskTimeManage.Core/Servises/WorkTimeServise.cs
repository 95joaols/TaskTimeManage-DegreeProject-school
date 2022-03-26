
using System.Runtime.Serialization;

using TaskTimeManage.Domain.Context;
using TaskTimeManage.Domain.Entity;
using TaskTimeManage.Domain.Enum;
using TaskTimeManage.Domain.Exceptions;

namespace TaskTimeManage.Core.Servises
{
    public class WorkTimeServise
    {
        private TTMDbContext context;

        public WorkTimeServise(TTMDbContext context)
        {
            this.context = context;
        }

        public async Task<WorkTime> CreateWorkTimeAsync(DateTime time, WorkTimeType type, Domain.Entity.Task task)
        {
            if (task is null)
            {
                throw new ArgumentNullException(nameof(task));
            }
            if (task.WorkTimes.Where(t => t.Type == WorkTimeType.Start).Count() > task.WorkTimes.Where(t => t.Type == WorkTimeType.End).Count())
                throw new TaskAredyStartedException();

            WorkTime workTime = new(time, type);
            task.WorkTimes.Add(workTime);
            context.Task.Update(task);   
            await context.SaveChangesAsync();

            return workTime;
        }
    }

    
}
