using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.CQRS.WorkItems.Commands;
using Application.CQRS.WorkTimes.Commands;

using Ardalis.GuardClauses;

using Domain.Aggregates.WorkAggregate;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.WorkItems.Handlers;
public class DeleteWorkItemHandler : IRequestHandler<DeleteWorkItemCommand, bool>
{
  private readonly IApplicationDbContext data;
  private readonly IMediator mediator;

  public DeleteWorkItemHandler(IApplicationDbContext data, IMediator mediator)
  {
    this.data = data;
    this.mediator = mediator;
  }
  public async Task<bool> Handle(DeleteWorkItemCommand request, CancellationToken cancellationToken)
  {
    _ = Guard.Against.Default(request.PublicId);

    WorkItem? workItem = await data.WorkItem.FirstOrDefaultAsync(t => t.PublicId == request.PublicId, cancellationToken);

    _ = Guard.Against.Null(workItem);

    if (!await mediator.Send(new DeleteAllWorkTimesByWorkItemIdCommand(workItem.Id), cancellationToken))
    {
      throw new UnableToDeleteWorkTimesException();
    }

    _ = data.WorkItem.Remove(workItem);
    return await data.SaveChangesAsync(cancellationToken) == 1;
  }
}
