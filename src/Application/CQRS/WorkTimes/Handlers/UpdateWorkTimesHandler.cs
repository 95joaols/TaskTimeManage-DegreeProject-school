using System.Xml.Linq;

namespace Application.CQRS.WorkTimes.Handlers;

public class UpdateWorkTimesHandler : IRequestHandler<UpdateWorkTimesCommand, IEnumerable<WorkTime>>
{
  private readonly IApplicationDbContext _data;
  private readonly IMediator _mediator;


  public UpdateWorkTimesHandler(IApplicationDbContext data, IMediator mediator)
  {
    _data = data;
    _mediator = mediator;
  }

  public async Task<IEnumerable<WorkTime>> Handle(UpdateWorkTimesCommand request, CancellationToken cancellationToken)
  {
    Guard.Against.NullOrEmpty(request.WorkTimes);
    Guard.Against.Default(request.WorkItemPublicId);

    var workItem = await _mediator.Send(new GetWorkItemWithWorkTimeByPublicIdQuery(request.WorkItemPublicId),
      cancellationToken
    );
    Guard.Against.Null(workItem);
    
    foreach (var workTime in workItem.WorkTimes)
    {
      DateTimeOffset? time = request.WorkTimes.FirstOrDefault(wt => wt.PublicId == workTime.PublicId)?.Time;
      if (time.HasValue)
      {
        workTime.UpdateTime(time.Value);
      }
    }
    _data.WorkItem.Update(workItem);
    await _data.SaveChangesAsync(cancellationToken);

    return workItem.WorkTimes;
  }
}