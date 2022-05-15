using Application.DataAccess;
using Application.Models;
using Application.Queries.WorkTimes;

using Ardalis.GuardClauses;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Application.Handlers.WorkTimes;
public class GetWorkTimesByWorkItemPublicIdHandler : IRequestHandler<GetWorkTimesByWorkItemPublicIdQuery, IEnumerable<WorkTimeModel>>
{
	private readonly TTMDataAccess data;

	public GetWorkTimesByWorkItemPublicIdHandler(TTMDataAccess data) => this.data = data;

	public async Task<IEnumerable<WorkTimeModel>> Handle(GetWorkTimesByWorkItemPublicIdQuery request, CancellationToken cancellationToken)
	{
		Guard.Against.Default(request.PublicId);

		return await data.WorkTime.Where(wt => wt.WorkItem.PublicId == request.PublicId).ToListAsync(cancellationToken: cancellationToken);
	}
}
