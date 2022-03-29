using TaskTimeManage.Domain.Context;
using TaskTimeManage.Domain.Entity;

namespace TaskTimeManage.Core.Service;

public class WorkItemService
{
	private readonly TTMDbContext context;

	public WorkItemService(TTMDbContext context) => this.context = context;

	public async Task<WorkItem> CreateTaskAsync(string name, User user)
	{
		WorkItem task = new(name, user);
		_ = await context.Task.AddAsync(task);
		_ = await context.SaveChangesAsync();

		return task;
	}
}
