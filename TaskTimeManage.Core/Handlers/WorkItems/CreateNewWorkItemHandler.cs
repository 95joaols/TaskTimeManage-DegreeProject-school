using MediatR;

using TaskTimeManage.Core.Commands.WorkItems;
using TaskTimeManage.Core.DataAccess;

using TaskTimeManage.Core.Models;
using TaskTimeManage.Core.Queries.Authentication;

namespace TaskTimeManage.Core.Handlers.WorkItems;
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
		Guard(request);
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
		await data.SaveChangesAsync(cancellationToken);


		return workItem;
	}

	private static void Guard(CreateNewWorkItemCommand request)
	{
		if (string.IsNullOrWhiteSpace(request.Name))
		{
			throw new ArgumentException($"'{nameof(request.Name)}' cannot be null or whitespace.", nameof(request.Name));
		}
		if (request.UserPublicId == Guid.Empty)
		{
			throw new ArgumentNullException($"'{nameof(request.UserPublicId)}' cannot be null or whitespace.", nameof(request.UserPublicId));
		}
	}
}
