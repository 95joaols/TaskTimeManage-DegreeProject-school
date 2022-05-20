namespace Application.CQRS.WorkTimes.Handlers;

public class DeleteAllWorkTimesByWorkItemIdHandler : IRequestHandler<DeleteAllWorkTimesByWorkItemIdCommand, bool>
{
  private readonly IApplicationDbContext _data;

  public DeleteAllWorkTimesByWorkItemIdHandler(IApplicationDbContext data) => _data = data;

  public async Task<bool> Handle(DeleteAllWorkTimesByWorkItemIdCommand request, CancellationToken cancellationToken)
  {
    Guard.Against.NegativeOrZero(request.WorkItemId);

    _data.WorkTime.RemoveRange(_data.WorkTime.Where(wt => wt.WorkItemId == request.WorkItemId));

    await _data.SaveChangesAsync(cancellationToken);

    return true;
  }
}