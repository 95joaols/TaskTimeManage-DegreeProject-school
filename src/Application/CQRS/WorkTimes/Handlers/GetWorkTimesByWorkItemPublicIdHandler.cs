using Application.CQRS.WorkTimes.Queries;

namespace Application.CQRS.WorkTimes.Handlers;

public class
  GetWorkTimesByWorkItemPublicIdHandler : IRequestHandler<GetWorkTimesByWorkItemPublicIdQuery, IEnumerable<WorkTime>>
{
  private readonly IApplicationDbContext _data;

  public GetWorkTimesByWorkItemPublicIdHandler(IApplicationDbContext data) => _data = data;

  public async Task<IEnumerable<WorkTime>> Handle(GetWorkTimesByWorkItemPublicIdQuery request,
    CancellationToken cancellationToken)
  {
    _ = Guard.Against.Default(request.PublicId);

    return await _data.WorkTime.Where(wt => wt.WorkItem.PublicId == request.PublicId).ToListAsync(cancellationToken);
  }
}