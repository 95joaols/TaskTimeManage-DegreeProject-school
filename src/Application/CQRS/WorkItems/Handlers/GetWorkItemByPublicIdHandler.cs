using Application.Common.Interfaces;
using Application.CQRS.WorkItems.Queries;

using Ardalis.GuardClauses;

using Domain.Entities;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.WorkItems.Handlers;
public class GetWorkItemByPublicIdHandler : IRequestHandler<GetWorkItemWithWorkTimeByPublicIdQuery, WorkItem?>
{
  private readonly IApplicationDbContext data;

  public GetWorkItemByPublicIdHandler(IApplicationDbContext data) => this.data = data;

  public async Task<WorkItem?> Handle(GetWorkItemWithWorkTimeByPublicIdQuery request, CancellationToken cancellationToken)
  {
    _ = Guard.Against.Default(request.PublicId);

    return await data.WorkItem.Include(x => x.WorkTimes).FirstOrDefaultAsync(wt => wt.PublicId == request.PublicId, cancellationToken: cancellationToken);
  }
}
