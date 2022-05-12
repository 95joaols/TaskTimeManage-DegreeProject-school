using MediatR;

using Microsoft.EntityFrameworkCore;

using TaskTimeManage.MediatR.Commands.WorkItems;
using TaskTimeManage.MediatR.Commands.WorkTimes;
using TaskTimeManage.MediatR.DataAccess;
using TaskTimeManage.MediatR.Models;

namespace TaskTimeManage.MediatR.Handlers.WorkItems;
public class DeleteWorkItemHandler : IRequestHandler<DeleteWorkItemCommand, bool>
{
	private readonly TTMDataAccess data;
	private readonly IMediator mediator;

	public DeleteWorkItemHandler(TTMDataAccess data, IMediator mediator)
	{
		this.data = data;
		this.mediator = mediator;
	}
	public async Task<bool> Handle(DeleteWorkItemCommand request, CancellationToken cancellationToken)
	{

		WorkItemModel? workItem = await data.WorkItem.FirstOrDefaultAsync(t => t.PublicId == request.PublicId, cancellationToken);

		if (workItem is null)
		{
			throw new ArgumentNullException(nameof(workItem));
		}
		if (!await mediator.Send(new DeleteAllWorkTimesByWorkItemIdCommand(workItem.Id), cancellationToken))
			throw new Exception("Error Cant delete All Work Times");

		data.WorkItem.Remove(workItem);
		return await data.SaveChangesAsync(cancellationToken) == 1;
	}
}
