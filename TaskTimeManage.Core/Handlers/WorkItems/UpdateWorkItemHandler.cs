using MediatR;

using Microsoft.EntityFrameworkCore;

using TaskTimeManage.Core.Commands.WorkItems;
using TaskTimeManage.Core.DataAccess;
using TaskTimeManage.Core.Models;

namespace TaskTimeManage.Core.Handlers.WorkItems;
public class UpdateWorkItemHandler : IRequestHandler<UpdateWorkItemCommand, WorkItemModel>
{
	private readonly TTMDataAccess data;

	public UpdateWorkItemHandler(TTMDataAccess data) => this.data = data;

	public async Task<WorkItemModel> Handle(UpdateWorkItemCommand request, CancellationToken cancellationToken)
	{
		if (request.PublicId == Guid.Empty)
			throw new ArgumentNullException($"'{nameof(request.PublicId)}' cannot be null", nameof(request.PublicId));
		if (string.IsNullOrWhiteSpace(request.Name.Trim()))
			throw new ArgumentNullException($"'{nameof(request.Name)}' cannot be null", nameof(request.Name));

		WorkItemModel? workItem = await data.WorkItem.FirstOrDefaultAsync(wi => wi.PublicId == request.PublicId, cancellationToken: cancellationToken);
		if (workItem == null)
			throw new ArgumentNullException(nameof(workItem));

		workItem.Name = request.Name.Trim();
		await data.SaveChangesAsync(cancellationToken);
		return workItem;
	}
}
