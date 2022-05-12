using MediatR;

using TaskTimeManage.MediatR.Commands.WorkTimes;
using TaskTimeManage.MediatR.DataAccess;
using TaskTimeManage.MediatR.Models;
using TaskTimeManage.MediatR.Queries.WorkItems;

namespace TaskTimeManage.MediatR.Handlers.WorkTimes;
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
		WorkItemModel? workItemModel = await mediator.Send(new GetWorkItemWithWorkTimeByPublicIdQuery(request.WorkItemPublicId), cancellationToken);
		if (workItemModel == null)
			throw new ArgumentException(nameof(workItemModel));

		WorkTimeModel workTimeModel = new WorkTimeModel { Time = request.Time, WorkItem = workItemModel };
		data.WorkTime.Add(workTimeModel);
		await data.SaveChangesAsync(cancellationToken);

		return workTimeModel;
	}
}
