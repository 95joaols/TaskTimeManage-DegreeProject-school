namespace Application.CQRS.WorkTimes.Handlers;

public class UpdateWorkTimesHandler : IRequestHandler<UpdateWorkTimesCommand, IEnumerable<WorkTime>>
{
  private readonly IApplicationDbContext _data;

  public UpdateWorkTimesHandler(IApplicationDbContext data) => _data = data;

  public async Task<IEnumerable<WorkTime>> Handle(UpdateWorkTimesCommand request, CancellationToken cancellationToken)
  {
    Guard.Against.NullOrEmpty(request.WorkTimes);

    IEnumerable<WorkTime> workTimes = await _data.WorkTime
      .Where(wt => request.WorkTimes.Select(x => x.PublicId).Contains(wt.PublicId)).ToListAsync(cancellationToken);

    foreach (var workTime in workTimes)
    {
      DateTimeOffset? time = request.WorkTimes.FirstOrDefault(wt => wt.PublicId == workTime.PublicId)?.Time;
      if (time.HasValue)
      {
        workTime.UpdateTime(time.Value);
      }
    }

    await _data.SaveChangesAsync(cancellationToken);

    return workTimes;
  }
}