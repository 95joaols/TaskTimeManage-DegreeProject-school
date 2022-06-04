namespace Application.CQRS.WorkTimes.Handlers;

public class DeleteWorkTimeByPublicIdHandler : IRequestHandler<DeleteWorkTimeByPublicIdCommand, bool>
{
  private readonly IApplicationDbContext _data;
  private readonly IMediator _mediator;


  public DeleteWorkTimeByPublicIdHandler(IApplicationDbContext data, IMediator mediator)
  {
    _data = data;
    _mediator = mediator;
  }

  public async Task<bool> Handle(DeleteWorkTimeByPublicIdCommand request, CancellationToken cancellationToken)
  {
    Guard.Against.Default(request.PublicId);
    Guard.Against.Default(request.WorkItemPublicId);

    var workItem = await _mediator.Send(new GetWorkItemWithWorkTimeByPublicIdQuery(request.WorkItemPublicId),
      cancellationToken
    );
    Guard.Against.Null(workItem);
    workItem.RemoveWorkTime(request.PublicId);

    _data.WorkItem.Update(workItem);
    return await _data.SaveChangesAsync(cancellationToken) == 1;
  }
}