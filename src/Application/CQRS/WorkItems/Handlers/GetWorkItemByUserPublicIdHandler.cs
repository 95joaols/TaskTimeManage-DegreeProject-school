using Application.Common.Interfaces;
using Application.CQRS.WorkItems.Queries;

using Ardalis.GuardClauses;

using Domain.Entities;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.WorkItems.Handlers;
public class GetWorkItemByUserPublicIdHandler : IRequestHandler<GetWorkItemTimeByUserPublicIdQuery, IEnumerable<WorkItem>>
{
	private readonly IApplicationDbContext data;

	public GetWorkItemByUserPublicIdHandler(IApplicationDbContext data) => this.data = data;
	public async Task<IEnumerable<WorkItem>> Handle(GetWorkItemTimeByUserPublicIdQuery request, CancellationToken cancellationToken)
	{
		_ = Guard.Against.Default(request.PublicId);

		return await data.WorkItem.Where(wt => wt.User.PublicId == request.PublicId).ToListAsync(cancellationToken);
	}
}
