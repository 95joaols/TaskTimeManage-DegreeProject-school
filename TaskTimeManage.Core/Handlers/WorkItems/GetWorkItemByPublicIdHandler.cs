using MediatR;

using Microsoft.EntityFrameworkCore;

using TaskTimeManage.Core.DataAccess;
using TaskTimeManage.Core.Models;
using TaskTimeManage.Core.Queries.WorkItems;

namespace TaskTimeManage.Core.Handlers.WorkItems;
public class GetWorkItemByPublicIdHandler : IRequestHandler<GetWorkItemWithWorkTimeByPublicIdQuery, WorkItemModel?>
{
	private readonly TTMDataAccess data;

	public GetWorkItemByPublicIdHandler(TTMDataAccess data) => this.data = data;

	public async Task<WorkItemModel?> Handle(GetWorkItemWithWorkTimeByPublicIdQuery request, CancellationToken cancellationToken)
	{
		if (request.PublicId == Guid.Empty)
		{
			throw new ArgumentNullException(nameof(request.PublicId));
		}

		return await data.WorkItem.Include(x => x.WorkTimes).FirstOrDefaultAsync(wt => wt.PublicId == request.PublicId, cancellationToken: cancellationToken);
	}
}
