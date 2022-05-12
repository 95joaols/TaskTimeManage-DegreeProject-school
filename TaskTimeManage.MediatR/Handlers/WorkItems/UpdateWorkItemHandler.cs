using MediatR;

using Microsoft.EntityFrameworkCore;

using TaskTimeManage.MediatR.Commands.WorkItems;
using TaskTimeManage.MediatR.DataAccess;
using TaskTimeManage.MediatR.Models;

namespace TaskTimeManage.MediatR.Handlers.WorkItems;
public class UpdateWorkItemHandler : IRequestHandler<UpdateWorkItemCommand, WorkItemModel>
{
	private readonly TTMDataAccess data;

	public UpdateWorkItemHandler(TTMDataAccess data) => this.data = data;

	public async Task<WorkItemModel> Handle(UpdateWorkItemCommand request, CancellationToken cancellationToken)
	{
		if (request.WorkItem == null)
			throw new ArgumentNullException($"'{nameof(request.WorkItem)}' cannot be null", nameof(request.WorkItem));

		WorkItemModel? workItem = await data.WorkItem.FirstOrDefaultAsync(wi => wi.PublicId == request.WorkItem.PublicId, cancellationToken: cancellationToken);
		if (workItem == null)
			throw new ArgumentNullException(nameof(workItem));

		workItem.Name = workItem.Name;
		await data.SaveChangesAsync(cancellationToken);
		return workItem;
	}
}
