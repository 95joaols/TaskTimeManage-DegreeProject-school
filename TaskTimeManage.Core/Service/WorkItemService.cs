using Microsoft.EntityFrameworkCore;
using TaskTimeManage.Domain.Context;
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

	public async Task<WorkItem?> GetWorkItemAsync(Guid publicId, CancellationToken cancellationToken)
	{
		return await context.Task.Include(T => T.User).FirstOrDefaultAsync(t => t.PublicId == publicId, cancellationToken: cancellationToken);
	}
}
