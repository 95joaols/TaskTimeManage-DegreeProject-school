using Application.Common.Interfaces;
using Application.CQRS.WorkTimes.Queries;

using Ardalis.GuardClauses;

using Domain.Entities;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.WorkTimes.Handlers;
public class GetWorkTimesByWorkItemPublicIdHandler : IRequestHandler<GetWorkTimesByWorkItemPublicIdQuery, IEnumerable<WorkTime>>
{
	private readonly IApplicationDbContext data;

	public GetWorkTimesByWorkItemPublicIdHandler(IApplicationDbContext data) => this.data = data;

	public async Task<IEnumerable<WorkTime>> Handle(GetWorkTimesByWorkItemPublicIdQuery request, CancellationToken cancellationToken)
	{
		Guard.Against.Default(request.PublicId);

		return await data.WorkTime.Where(wt => wt.WorkItem.PublicId == request.PublicId).ToListAsync(cancellationToken: cancellationToken);
	}
}
