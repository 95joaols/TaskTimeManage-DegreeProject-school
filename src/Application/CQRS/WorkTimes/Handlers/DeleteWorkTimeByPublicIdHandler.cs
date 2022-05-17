using Application.Common.Interfaces;
using Application.CQRS.WorkTimes.Commands;
using Ardalis.GuardClauses;
using Domain.Aggregates.WorkAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.WorkTimes.Handlers;

public class DeleteWorkTimeByPublicIdHandler : IRequestHandler<DeleteWorkTimeByPublicIdCommand, bool>
{
  private readonly IApplicationDbContext _data;

  public DeleteWorkTimeByPublicIdHandler(IApplicationDbContext data) => _data = data;

  public async Task<bool> Handle(DeleteWorkTimeByPublicIdCommand request, CancellationToken cancellationToken)
  {
    _ = Guard.Against.Default(request.PublicId);

    WorkTime? workTime =
      await _data.WorkTime.FirstOrDefaultAsync(x => x.PublicId == request.PublicId, cancellationToken);
    _ = Guard.Against.Null(workTime);


    _ = _data.WorkTime.Remove(workTime);
    return await _data.SaveChangesAsync(cancellationToken) == 1;
  }
}