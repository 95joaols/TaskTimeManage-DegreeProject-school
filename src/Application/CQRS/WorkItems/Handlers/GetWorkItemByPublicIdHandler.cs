using Application.Common.Interfaces;
using Application.CQRS.WorkItems.Queries;
using Ardalis.GuardClauses;
using Domain.Aggregates.WorkAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.WorkItems.Handlers;

public class GetWorkItemByPublicIdHandler : IRequestHandler<GetWorkItemWithWorkTimeByPublicIdQuery, WorkItem?>
{
  private readonly IApplicationDbContext _data;

  public GetWorkItemByPublicIdHandler(IApplicationDbContext data) => _data = data;

  public async Task<WorkItem?> Handle(GetWorkItemWithWorkTimeByPublicIdQuery request,
    CancellationToken cancellationToken)
  {
    _ = Guard.Against.Default(request.PublicId);

    return await _data.WorkItem.Include(x => x.WorkTimes)
      .FirstOrDefaultAsync(wt => wt.PublicId == request.PublicId, cancellationToken);
  }
}