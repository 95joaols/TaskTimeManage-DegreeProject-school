namespace Application.CQRS.WorkItems.Handlers;

public class GetWorkItemByPublicIdHandler : IRequestHandler<GetWorkItemWithWorkTimeByPublicIdQuery, WorkItem?>
{
  private readonly IApplicationDbContext _data;

  public GetWorkItemByPublicIdHandler(IApplicationDbContext data) => _data = data;

  public async Task<WorkItem?> Handle(GetWorkItemWithWorkTimeByPublicIdQuery request,
    CancellationToken cancellationToken)
  {
    Guard.Against.Default(request.PublicId);

    return await _data.WorkItem
      .FirstOrDefaultAsync(wt => wt.PublicId == request.PublicId, cancellationToken);
  }
}