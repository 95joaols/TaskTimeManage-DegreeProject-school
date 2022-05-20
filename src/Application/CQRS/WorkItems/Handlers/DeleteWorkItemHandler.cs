namespace Application.CQRS.WorkItems.Handlers;

public class DeleteWorkItemHandler : IRequestHandler<DeleteWorkItemCommand, bool>
{
  private readonly IApplicationDbContext _data;
  private readonly IMediator _mediator;

  public DeleteWorkItemHandler(IApplicationDbContext data, IMediator mediator)
  {
    _data = data;
    _mediator = mediator;
  }

  public async Task<bool> Handle(DeleteWorkItemCommand request, CancellationToken cancellationToken)
  {
    _ = Guard.Against.Default(request.PublicId);

    WorkItem? workItem =
      await _data.WorkItem.FirstOrDefaultAsync(t => t.PublicId == request.PublicId, cancellationToken);

    _ = Guard.Against.Null(workItem);

    if (!await _mediator.Send(new DeleteAllWorkTimesByWorkItemIdCommand(workItem.Id), cancellationToken))
    {
      throw new UnableToDeleteWorkTimesException();
    }

    _ = _data.WorkItem.Remove(workItem);
    return await _data.SaveChangesAsync(cancellationToken) == 1;
  }
}