using Application.Commands.WorkItems;
using Application.DataAccess;
using Application.Models;

using Ardalis.GuardClauses;

using MediatR;

using TaskTimeManage.Core.Queries.Authentication;

namespace Application.Handlers.WorkItems;
public class CreateNewWorkItemHandler : IRequestHandler<CreateNewWorkItemCommand, WorkItemModel>
{
	private readonly TTMDataAccess data;
	private readonly IMediator mediator;

	public CreateNewWorkItemHandler(TTMDataAccess data, IMediator mediator)
	{
		this.data = data;
		this.mediator = mediator;
	}

	public async Task<WorkItemModel> Handle(CreateNewWorkItemCommand request, CancellationToken cancellationToken)
	{
		Guard.Against.NullOrWhiteSpace(request.Name);
		Guard.Against.Default(request.UserPublicId);

		UserModel? userModel = await mediator.Send(new GetUserByPublicIdQuery(request.UserPublicId), cancellationToken);
		if (userModel == null)
		{
			throw new ArgumentException(nameof(request.UserPublicId));
		}

		WorkItemModel workItem = new() {
			Name = request.Name.Trim(),
			User = userModel,
		};
		_ = await data.WorkItem.AddAsync(workItem, cancellationToken);
		_ = await data.SaveChangesAsync(cancellationToken);


		return workItem;
	}
}
