using Microsoft.EntityFrameworkCore;

using TaskTimeManage.Domain.Context;
using TaskTimeManage.Domain.Dto;
using TaskTimeManage.Domain.Entity;

namespace TaskTimeManage.Core.Service;

public class WorkItemService
{
	private readonly TTMDbContext context;

	public WorkItemService(TTMDbContext context) => this.context = context;

	public async Task<WorkItem> CreateTaskAsync(string name, User user, CancellationToken cancellationToken)
	{
		WorkItem task = new(name, user);
		_ = await context.Task.AddAsync(task, cancellationToken);
		_ = await context.SaveChangesAsync(cancellationToken);

		return task;
	}

	public async Task<WorkItem> UpdateAsync(WorkItemDto workItem, CancellationToken cancellationToken)
	{
		WorkItem? workItemToUpdate = await context.Task.FirstOrDefaultAsync(t => t.PublicId == workItem.PublicId, cancellationToken: cancellationToken);
		if (workItemToUpdate is null)
		{
			throw new ArgumentNullException(nameof(workItemToUpdate));
		}
		workItemToUpdate.Name = workItem.Name;
		if (workItem.WorkTimes is not null)
		{
			workItemToUpdate.WorkTimes = workItem.WorkTimes;
		}

		await context.SaveChangesAsync(cancellationToken);
		return workItemToUpdate;
	}

	public async Task<bool> DeleteWorkItemAsync(Guid publicId, CancellationToken cancellationToken)
	{
		WorkItem? task = await context.Task.FirstOrDefaultAsync(T => T.PublicId == publicId, cancellationToken: cancellationToken);
		if (task is null)
		{
			throw new ArgumentNullException(nameof(task));
		}
		context.Task.Remove(task);
		return await context.SaveChangesAsync(cancellationToken) == 1;
	}


	public async Task<WorkItem?> GetWorkItemAsync(Guid publicId, CancellationToken cancellationToken)
	{
		return await context.Task.Include(T => T.User).FirstOrDefaultAsync(t => t.PublicId == publicId, cancellationToken: cancellationToken);
	}
}
