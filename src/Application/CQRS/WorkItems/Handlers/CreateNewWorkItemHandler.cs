using Application.Common.Interfaces;
using Application.CQRS.Authentication.Queries;
using Application.CQRS.WorkItems.Commands;

using Ardalis.GuardClauses;

using Domain.Entities;

using MediatR;

namespace Application.CQRS.WorkItems.Handlers;
public class CreateNewWorkItemHandler : IRequestHandler<CreateNewWorkItemCommand, WorkItem>
{
  private readonly IApplicationDbContext data;
  private readonly IMediator mediator;

  public CreateNewWorkItemHandler(IApplicationDbContext data, IMediator mediator)
  {
    this.data = data;
    this.mediator = mediator;
  }

  public async Task<WorkItem> Handle(CreateNewWorkItemCommand request, CancellationToken cancellationToken)
  {
    _ = Guard.Against.NullOrWhiteSpace(request.Name);
    _ = Guard.Against.Default(request.UserPublicId);

    User? User = await mediator.Send(new GetUserByPublicIdQuery(request.UserPublicId), cancellationToken);
    if (User == null)
    {
      throw new ArgumentException(nameof(request.UserPublicId));
    }

    WorkItem workItem = new() {
      Name = request.Name.Trim(),
      User = User,
    };
    _ = await data.WorkItem.AddAsync(workItem, cancellationToken);
    _ = await data.SaveChangesAsync(cancellationToken);


    return workItem;
  }
}
