namespace Application.CQRS.WorkItems.Handlers;

public class UpdateWorkItemHandler : IRequestHandler<UpdateWorkItemCommand, WorkItem>
{
  private readonly IApplicationDbContext _data;

  public UpdateWorkItemHandler(IApplicationDbContext data) => _data = data;

  public async Task<WorkItem> Handle(UpdateWorkItemCommand request, CancellationToken cancellationToken)
  {
    Guard.Against.Default(request.PublicId);
    Guard.Against.NullOrWhiteSpace(request.Name);
    
    var workItem =
      await _data.WorkItem.FirstOrDefaultAsync(wi => wi.PublicId == request.PublicId, cancellationToken);

    Guard.Against.Null(workItem);

    workItem.UpdateName(request.Name);
    await _data.SaveChangesAsync(cancellationToken);

    return workItem;
  }
}