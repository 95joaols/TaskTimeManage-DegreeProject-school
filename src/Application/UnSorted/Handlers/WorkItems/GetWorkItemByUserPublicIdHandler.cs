using Application.DataAccess;
using Application.Models;
using Application.Queries.WorkItems;

using Ardalis.GuardClauses;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Application.Handlers.WorkItems;
public class GetWorkItemByUserPublicIdHandler : IRequestHandler<GetWorkItemTimeByUserPublicIdQuery, IEnumerable<WorkItemModel>>
{
	private readonly TTMDataAccess data;

	public GetWorkItemByUserPublicIdHandler(TTMDataAccess data) => this.data = data;
	public async Task<IEnumerable<WorkItemModel>> Handle(GetWorkItemTimeByUserPublicIdQuery request, CancellationToken cancellationToken)
	{
		Guard.Against.Default(request.PublicId);

		return await data.WorkItem.Where(wt => wt.User.PublicId == request.PublicId).ToListAsync(cancellationToken);
	}
}
