using Application.Common.Interfaces;
using Application.CQRS.WorkTimes.Commands;
using Ardalis.GuardClauses;
using Domain.Aggregates.WorkAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.WorkTimes.Handlers;

public class UpdateWorkTimesHandler : IRequestHandler<UpdateWorkTimesCommand, IEnumerable<WorkTime>>
{
  private readonly IApplicationDbContext _data;

  public UpdateWorkTimesHandler(IApplicationDbContext data) => _data = data;

  public async Task<IEnumerable<WorkTime>> Handle(UpdateWorkTimesCommand request, CancellationToken cancellationToken)
  {
    _ = Guard.Against.NullOrEmpty(request.WorkTimes);

    IEnumerable<WorkTime> workTimes = await _data.WorkTime
      .Where(wt => request.WorkTimes.Select(x => x.PublicId).Contains(wt.PublicId)).ToListAsync(cancellationToken);

    foreach (WorkTime? workTime in workTimes)
    {
      DateTimeOffset? time = request.WorkTimes.FirstOrDefault(wt => wt.PublicId == workTime.PublicId)?.Time;
      if (time.HasValue)
      {
        workTime.UpdateTime(time.Value);
      }
    }

    _ = await _data.SaveChangesAsync(cancellationToken);
    return workTimes;
  }
}