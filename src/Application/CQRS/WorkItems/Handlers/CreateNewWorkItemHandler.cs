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
    Guard.Against.NullOrWhiteSpace(request.Name);
    Guard.Against.Default(request.UserPublicId);

    var user = await _mediator.Send(new GetUserByPublicIdQuery(request.UserPublicId), cancellationToken);
    if (user == null)
    {
      throw new ArgumentException(nameof(request.UserPublicId));
    }

    var workItem = WorkItem.CreateWorkItem(request.Name, user);

    await _data.WorkItem.AddAsync(workItem, cancellationToken);
    await _data.SaveChangesAsync(cancellationToken);


    return workItem;
  }
}