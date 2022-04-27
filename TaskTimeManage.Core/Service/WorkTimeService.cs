using Microsoft.EntityFrameworkCore;
using TaskTimeManage.Domain.Context;
using TaskTimeManage.Domain.Entity;
using TaskTimeManage.Domain.Enum;
using TaskTimeManage.Domain.Exceptions;

namespace TaskTimeManage.Core.Service;

public class WorkTimeService
{
	private readonly TTMDbContext context;

	public WorkTimeService(TTMDbContext context) => this.context = context;

	public async Task<WorkTime> CreateWorkTimeAsync(DateTime time, WorkItem task, CancellationToken cancellationToken)
	{
		if (task is null)
		{
			throw new ArgumentNullException(nameof(task));
		}

		WorkTime workTime = new(time);
		task.WorkTimes.Add(workTime);
		_ = context.Task.Update(task);
		_ = await context.SaveChangesAsync(cancellationToken);

		return workTime;
	}
	public async Task<WorkTime> CreateWorkTimeAsync(WorkTime workTime, Guid WorkPublicId, CancellationToken cancellationToken)
	{
		WorkItem? task = await context.Task.FirstOrDefaultAsync(T => T.PublicId == WorkPublicId, cancellationToken: cancellationToken);

		if (task is null)
		{
			throw new ArgumentNullException(nameof(task));
		}
		task.WorkTimes.Add(workTime);
		_ = context.Task.Update(task);
		_ = await context.SaveChangesAsync(cancellationToken);

		return workTime;
	}
}


