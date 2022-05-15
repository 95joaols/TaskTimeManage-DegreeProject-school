using Application.DataAccess;
using Application.Models;
using Application.Queries.WorkItems;

using Ardalis.GuardClauses;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Application.Handlers.WorkItems;
public class GetWorkItemByPublicIdHandler : IRequestHandler<GetWorkItemWithWorkTimeByPublicIdQuery, WorkItemModel?>
{
	private readonly TTMDataAccess data;

	public GetWorkItemByPublicIdHandler(TTMDataAccess data) => this.data = data;

	public async Task<WorkItemModel?> Handle(GetWorkItemWithWorkTimeByPublicIdQuery request, CancellationToken cancellationToken)
	{
		Guard.Against.Default(request.PublicId);

		return await data.WorkItem.Include(x => x.WorkTimes).FirstOrDefaultAsync(wt => wt.PublicId == request.PublicId, cancellationToken: cancellationToken);
	}
}
