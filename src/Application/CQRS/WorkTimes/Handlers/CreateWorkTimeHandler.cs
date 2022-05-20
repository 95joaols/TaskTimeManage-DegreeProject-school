namespace Application.CQRS.WorkTimes.Handlers;

public class CreateWorkTimeHandler : IRequestHandler<CreateWorkTimeCommand, WorkTime>
{
  private readonly IApplicationDbContext _data;
  private readonly IMediator _mediator;

  public CreateWorkTimeHandler(IApplicationDbContext data, IMediator mediator)
  {
    _data = data;
    _mediator = mediator;
  }

  public async Task<WorkTime> Handle(CreateWorkTimeCommand request, CancellationToken cancellationToken)
  {
    Guard.Against.Default(request.WorkItemPublicId);

    var workItem = await _mediator.Send(new GetWorkItemWithWorkTimeByPublicIdQuery(request.WorkItemPublicId),
      cancellationToken
    );
    Guard.Against.Null(workItem);


    var workTime = WorkTime.CreateWorkTime(request.Time, workItem);

    _data.WorkTime.Add(workTime);
    await _data.SaveChangesAsync(cancellationToken);

    return workTime;
  }
}