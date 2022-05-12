using MediatR;

using TaskTimeManage.MediatR.Commands.WorkItems;
using TaskTimeManage.MediatR.DataAccess;
using TaskTimeManage.MediatR.Models;

namespace TaskTimeManage.MediatR.Handlers.WorkItems;
public class CreateNewWorkItemHandler : IRequestHandler<CreateNewWorkItemCommand, WorkItemModel>
{
	private readonly TTMDataAccess data;

	public CreateNewWorkItemHandler(TTMDataAccess data) => this.data = data;
	public async Task<WorkItemModel> Handle(CreateNewWorkItemCommand request, CancellationToken cancellationToken)
	{
		Guard(request);

		WorkItemModel workItem = new() {
			Name = request.Name,
			User = request.User,
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
		if (request.User == null)
		{
			throw new ArgumentNullException($"'{nameof(request.User)}' cannot be null or whitespace.", nameof(request.User));
		}
	}
}
