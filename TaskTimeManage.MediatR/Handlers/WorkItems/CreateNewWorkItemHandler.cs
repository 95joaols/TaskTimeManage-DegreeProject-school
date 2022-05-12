﻿using MediatR;

using TaskTimeManage.MediatR.Commands.WorkItems;
using TaskTimeManage.MediatR.DataAccess;
using TaskTimeManage.MediatR.Models;
using TaskTimeManage.MediatR.Queries.Authentication;

namespace TaskTimeManage.MediatR.Handlers.WorkItems;
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
			Name = request.Name,
			User = userModel,
		};
		_ = await data.WorkItem.AddAsync(workItem, cancellationToken);


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
