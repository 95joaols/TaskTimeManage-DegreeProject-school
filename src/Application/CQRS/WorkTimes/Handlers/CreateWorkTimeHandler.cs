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
    _ = Guard.Against.Default(request.WorkItemPublicId);

    WorkItem? workItem = await _mediator.Send(new GetWorkItemWithWorkTimeByPublicIdQuery(request.WorkItemPublicId),
      cancellationToken
    );
    _ = Guard.Against.Null(workItem);


    WorkTime workTime = WorkTime.CreateWorkTime(request.Time, workItem);

    _ = _data.WorkTime.Add(workTime);
    _ = await _data.SaveChangesAsync(cancellationToken);

    return workTime;
  }
}