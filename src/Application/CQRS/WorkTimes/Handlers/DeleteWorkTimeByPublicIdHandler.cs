namespace Application.CQRS.WorkTimes.Handlers;

public class DeleteWorkTimeByPublicIdHandler : IRequestHandler<DeleteWorkTimeByPublicIdCommand, bool>
{
  private readonly IApplicationDbContext _data;

  public DeleteWorkTimeByPublicIdHandler(IApplicationDbContext data) => _data = data;

  public async Task<bool> Handle(DeleteWorkTimeByPublicIdCommand request, CancellationToken cancellationToken)
  {
    Guard.Against.Default(request.PublicId);

    var workTime =
      await _data.WorkTime.FirstOrDefaultAsync(x => x.PublicId == request.PublicId, cancellationToken);
    Guard.Against.Null(workTime);


    _data.WorkTime.Remove(workTime);

    return await _data.SaveChangesAsync(cancellationToken) == 1;
  }
}