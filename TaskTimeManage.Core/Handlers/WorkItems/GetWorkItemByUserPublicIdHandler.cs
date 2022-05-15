using MediatR;

using Microsoft.EntityFrameworkCore;

using TaskTimeManage.Core.DataAccess;

using TaskTimeManage.Core.Models;
using TaskTimeManage.Core.Queries.WorkItems;

namespace TaskTimeManage.Core.Handlers.WorkItems;
public class GetWorkItemByUserPublicIdHandler : IRequestHandler<GetWorkItemTimeByUserPublicIdQuery, IEnumerable<WorkItemModel>>
{
	private readonly TTMDataAccess data;

	public GetWorkItemByUserPublicIdHandler(TTMDataAccess data) => this.data = data;
	public async Task<IEnumerable<WorkItemModel>> Handle(GetWorkItemTimeByUserPublicIdQuery request, CancellationToken cancellationToken)
	{
		if (request.PublicId == Guid.Empty)
		{
			throw new ArgumentNullException(nameof(request.PublicId));
		}

		return await data.WorkItem.Where(wt => wt.User.PublicId == request.PublicId).ToListAsync(cancellationToken);


	}
}
