using Ardalis.GuardClauses;

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
		Guard.Against.Default(request.PublicId);

		Guard.Against.NullOrWhiteSpace(request.Name);


		WorkItemModel? workItem = await data.WorkItem.FirstOrDefaultAsync(wi => wi.PublicId == request.PublicId, cancellationToken: cancellationToken);

		Guard.Against.Null(workItem);


		workItem.Name = request.Name.Trim();
		_ = await data.SaveChangesAsync(cancellationToken);
		return workItem;
	}
}
