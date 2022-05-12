using MediatR;

using Microsoft.EntityFrameworkCore;

using TaskTimeManage.MediatR.DataAccess;
using TaskTimeManage.MediatR.Models;
using TaskTimeManage.MediatR.Queries.WorkTimes;

namespace TaskTimeManage.MediatR.Handlers.WorkTimes;
internal class GetWorkTimesByWorkItemPublicIdHandler : IRequestHandler<GetWorkTimesByWorkItemPublicIdQuery, IEnumerable<WorkTimeModel>>
{
	private readonly TTMDataAccess data;

	public GetWorkTimesByWorkItemPublicIdHandler(TTMDataAccess data) => this.data = data;

	public async Task<IEnumerable<WorkTimeModel>> Handle(GetWorkTimesByWorkItemPublicIdQuery request, CancellationToken cancellationToken)
	{
		if (request.PublicId == Guid.Empty)
			throw new ArgumentNullException(nameof(request.PublicId));

		return await data.WorkTime.Where(wt =>wt.WorkItem.PublicId == request.PublicId).ToListAsync(cancellationToken: cancellationToken);
	}
}
