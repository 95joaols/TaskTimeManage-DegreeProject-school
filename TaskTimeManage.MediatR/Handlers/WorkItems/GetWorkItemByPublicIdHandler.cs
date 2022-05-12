using MediatR;

using Microsoft.EntityFrameworkCore;

using TaskTimeManage.MediatR.DataAccess;
using TaskTimeManage.MediatR.Models;
using TaskTimeManage.MediatR.Queries.WorkItems;

namespace TaskTimeManage.MediatR.Handlers.WorkItems;
public class GetWorkItemByPublicIdHandler : IRequestHandler<GetWorkItemWithWorkTimeByPublicIdQuery, WorkItemModel?>
{
	private readonly TTMDataAccess data;

	public GetWorkItemByPublicIdHandler(TTMDataAccess data) => this.data = data;

	public async Task<WorkItemModel?> Handle(GetWorkItemWithWorkTimeByPublicIdQuery request, CancellationToken cancellationToken)
	{
		if(request.PublicId == Guid.Empty) throw new ArgumentNullException(nameof(request.PublicId));

		return await data.WorkItem.Include(x => x.WorkTimes).FirstOrDefaultAsync(wt => wt.PublicId == request.PublicId, cancellationToken: cancellationToken);
	}
}
