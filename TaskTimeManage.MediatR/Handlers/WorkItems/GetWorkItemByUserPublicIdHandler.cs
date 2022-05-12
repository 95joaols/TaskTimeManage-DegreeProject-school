using MediatR;

using Microsoft.EntityFrameworkCore;

using TaskTimeManage.MediatR.DataAccess;
using TaskTimeManage.MediatR.Models;
using TaskTimeManage.MediatR.Queries.WorkItems;

namespace TaskTimeManage.MediatR.Handlers.WorkItems;
public class GetWorkItemByUserPublicIdHandler : IRequestHandler<GetWorkItemTimeByUserPublicIdQuery, IEnumerable<WorkItemModel>>
{
	private readonly TTMDataAccess data;

	public GetWorkItemByUserPublicIdHandler(TTMDataAccess data) => this.data = data;
	public async Task<IEnumerable<WorkItemModel>> Handle(GetWorkItemTimeByUserPublicIdQuery request, CancellationToken cancellationToken)
	{
		if (request.PublicId == Guid.Empty)
			throw new ArgumentNullException(nameof(request.PublicId));

		return await data.WorkItem.Where(wt => wt.User.PublicId == request.PublicId).ToListAsync(cancellationToken);


	}
}
