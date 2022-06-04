namespace Application.CQRS.WorkItems.Handlers;

public class DeleteWorkItemHandler : IRequestHandler<DeleteWorkItemCommand, bool>
{
  private readonly IApplicationDbContext _data;

  public DeleteWorkItemHandler(IApplicationDbContext data)
  {
    _data = data;
  }

  public async Task<bool> Handle(DeleteWorkItemCommand request, CancellationToken cancellationToken)
  {
    Guard.Against.Default(request.PublicId);

    var workItem =
      await _data.WorkItem.FirstOrDefaultAsync(t => t.PublicId == request.PublicId, cancellationToken);
    Guard.Against.Null(workItem);

    _data.WorkItem.Remove(workItem);

    return await _data.SaveChangesAsync(cancellationToken) == 1;
  }
}