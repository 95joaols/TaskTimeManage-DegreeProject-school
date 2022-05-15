using Application.Commands.WorkItems;
using Application.DataAccess;
using Application.Models;

using Ardalis.GuardClauses;

using MediatR;

using Microsoft.EntityFrameworkCore;

using TaskTimeManage.Core.Commands.WorkTimes;
using TaskTimeManage.Core.Exceptions;

namespace Application.Handlers.WorkItems;
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
		Guard.Against.Default(request.PublicId);

		WorkItemModel? workItem = await data.WorkItem.FirstOrDefaultAsync(t => t.PublicId == request.PublicId, cancellationToken);

		Guard.Against.Null(workItem);

		if (!await mediator.Send(new DeleteAllWorkTimesByWorkItemIdCommand(workItem.Id), cancellationToken))
		{
			throw new UnableToDeleteWorkTimesException();
		}

		_ = data.WorkItem.Remove(workItem);
		return await data.SaveChangesAsync(cancellationToken) == 1;
	}
}
