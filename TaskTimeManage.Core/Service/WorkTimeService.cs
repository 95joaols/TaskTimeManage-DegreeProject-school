using TaskTimeManage.Domain.Context;
using TaskTimeManage.Domain.Entity;
using TaskTimeManage.Domain.Enum;
using TaskTimeManage.Domain.Exceptions;

namespace TaskTimeManage.Core.Service;

public class WorkTimeService
{
	private readonly TTMDbContext context;

	public WorkTimeService(TTMDbContext context) => this.context = context;

	public async Task<WorkTime> CreateWorkTimeAsync(DateTime time, WorkTimeType type, WorkItem task)
	{
		if (task is null)
		{
			throw new ArgumentNullException(nameof(task));
		}
		if (task.WorkTimes.Where(t => t.Type == WorkTimeType.Start).Count() > task.WorkTimes.Where(t => t.Type == WorkTimeType.End).Count())
		{
			throw new TaskAredyStartedException();
		}

		WorkTime workTime = new(time, type);
		task.WorkTimes.Add(workTime);
		_ = context.Task.Update(task);
		_ = await context.SaveChangesAsync();

		return workTime;
	}
}


