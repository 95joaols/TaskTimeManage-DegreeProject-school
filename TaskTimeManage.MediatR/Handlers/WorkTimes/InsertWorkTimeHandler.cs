using MediatR;

using TaskTimeManage.MediatR.Commands.WorkTimes;
using TaskTimeManage.MediatR.DataAccess;
using TaskTimeManage.MediatR.Models;
using TaskTimeManage.MediatR.Queries.WorkItems;

namespace TaskTimeManage.MediatR.Handlers.WorkTimes;
public class InsertWorkTimeHandler : IRequestHandler<InsertWorkTimeCommand, WorkTimeModel>
{
	private readonly TTMDataAccess data;
	private readonly IMediator mediator;

	public InsertWorkTimeHandler(TTMDataAccess data, IMediator mediator)
	{
		this.data = data;
		this.mediator = mediator;
	}

	public async Task<WorkTimeModel> Handle(InsertWorkTimeCommand request, CancellationToken cancellationToken)
	{
		WorkItemModel? workItemModel = await mediator.Send(new GetWorkItemByPublicIdQuery(request.WorkItemPublicId), cancellationToken);
		if (workItemModel == null)
			throw new ArgumentException(nameof(workItemModel));

		request.WorkTimes.WorkItem = workItemModel;
		data.WorkTime.Add(request.WorkTimes);
		await data.SaveChangesAsync(cancellationToken);

		return request.WorkTimes;
	}
}
