namespace Application.CQRS.WorkItems.Handlers;

public class UpdateWorkItemHandler : IRequestHandler<UpdateWorkItemCommand, WorkItem>
{
  private readonly IApplicationDbContext _data;

  public UpdateWorkItemHandler(IApplicationDbContext data) => _data = data;

  public async Task<WorkItem> Handle(UpdateWorkItemCommand request, CancellationToken cancellationToken)
  {
    _ = Guard.Against.Default(request.PublicId);

    _ = Guard.Against.NullOrWhiteSpace(request.Name);


    WorkItem? workItem =
      await _data.WorkItem.FirstOrDefaultAsync(wi => wi.PublicId == request.PublicId, cancellationToken);

    _ = Guard.Against.Null(workItem);

    workItem.UpdateName(request.Name);
    _ = await _data.SaveChangesAsync(cancellationToken);
    return workItem;
  }
}