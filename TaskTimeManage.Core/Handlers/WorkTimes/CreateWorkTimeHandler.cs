using Ardalis.GuardClauses;

using MediatR;

using TaskTimeManage.Core.Commands.WorkTimes;
using TaskTimeManage.Core.DataAccess;
using TaskTimeManage.Core.Models;
using TaskTimeManage.Core.Queries.WorkItems;

namespace TaskTimeManage.Core.Handlers.WorkTimes;
public class CreateWorkTimeHandler : IRequestHandler<CreateWorkTimeCommand, WorkTimeModel>
{
	private readonly TTMDataAccess data;
	private readonly IMediator mediator;

	public CreateWorkTimeHandler(TTMDataAccess data, IMediator mediator)
	{
		this.data = data;
		this.mediator = mediator;
	}

	public async Task<WorkTimeModel> Handle(CreateWorkTimeCommand request, CancellationToken cancellationToken)
	{
		Guard.Against.Default(request.WorkItemPublicId);

		WorkItemModel? workItemModel = await mediator.Send(new GetWorkItemWithWorkTimeByPublicIdQuery(request.WorkItemPublicId), cancellationToken);
		Guard.Against.Null(workItemModel);


		WorkTimeModel workTimeModel = new() {
			Time = request.Time,
			WorkItem = workItemModel
		};
		_ = data.WorkTime.Add(workTimeModel);
		_ = await data.SaveChangesAsync(cancellationToken);

		return workTimeModel;
	}
}
