namespace Application.CQRS.WorkItems.Handlers;

public class CreateNewWorkItemHandler : IRequestHandler<CreateNewWorkItemCommand, WorkItem>
{
  private readonly IApplicationDbContext _data;
  private readonly IMediator _mediator;

  public CreateNewWorkItemHandler(IApplicationDbContext data, IMediator mediator)
  {
    _data = data;
    _mediator = mediator;
  }

  public async Task<WorkItem> Handle(CreateNewWorkItemCommand request, CancellationToken cancellationToken)
  {
    _ = Guard.Against.NullOrWhiteSpace(request.Name);
    _ = Guard.Against.Default(request.UserPublicId);

    UserProfile? user = await _mediator.Send(new GetUserByPublicIdQuery(request.UserPublicId), cancellationToken);
    if (user == null)
    {
      throw new ArgumentException(nameof(request.UserPublicId));
    }

    WorkItem workItem = WorkItem.CreateWorkItem(request.Name, user);

    _ = await _data.WorkItem.AddAsync(workItem, cancellationToken);
    _ = await _data.SaveChangesAsync(cancellationToken);


    return workItem;
  }
}