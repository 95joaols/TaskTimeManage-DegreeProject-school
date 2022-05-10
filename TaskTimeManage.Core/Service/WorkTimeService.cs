using Microsoft.EntityFrameworkCore;

using TaskTimeManage.Domain.Context;
using TaskTimeManage.Domain.Entity;

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

		WorkTime workTime = new(time, Guid.NewGuid());
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
		workTime.PublicId = Guid.NewGuid();

		task.WorkTimes.Add(workTime);
		_ = context.Task.Update(task);
		_ = await context.SaveChangesAsync(cancellationToken);

		return workTime;
	}

	public async Task<WorkTime> EditWorkTime(WorkTime workTime, Guid WorkPublicId, CancellationToken cancellationToken)
	{
		WorkItem? task = await context.Task.FirstOrDefaultAsync(T => T.PublicId == WorkPublicId, cancellationToken: cancellationToken);

		if (task is null)
		{
			throw new ArgumentNullException(nameof(task));
		}
		WorkTime? workTimetoUpdate = task.WorkTimes.FirstOrDefault(wt => wt.PublicId == workTime.PublicId);
		if (workTimetoUpdate is null)
		{
			throw new ArgumentNullException(nameof(workTimetoUpdate));
		}

		workTimetoUpdate.Time = workTime.Time;

		_ = context.Task.Update(task);
		_ = await context.SaveChangesAsync(cancellationToken);

		return workTimetoUpdate;
	}

	public async Task<bool> DeleteWorkTimeAsync(Guid publicId, Guid WorkItemId, CancellationToken cancellationToken)
	{

		WorkItem? workItem = await context.Task.FirstOrDefaultAsync(T => T.PublicId == WorkItemId, cancellationToken: cancellationToken);
		if (workItem == null)
		{
			return false;
		}
		workItem.WorkTimes.RemoveAll(wt => wt.PublicId == publicId);
		context.Entry(workItem).State = EntityState.Modified;

		await context.SaveChangesAsync(cancellationToken);

		return true;

	}

}


