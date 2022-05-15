using Application.Common.Interfaces;
using Application.CQRS.WorkTimes.Commands;

using Ardalis.GuardClauses;

using Domain.Entities;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.WorkTimes.Handlers;
public class DeleteWorkTimeByPublicIdHandler : IRequestHandler<DeleteWorkTimeByPublicIdCommand, bool>
{

	private readonly IApplicationDbContext data;

	public DeleteWorkTimeByPublicIdHandler(IApplicationDbContext data) => this.data = data;

	public async Task<bool> Handle(DeleteWorkTimeByPublicIdCommand request, CancellationToken cancellationToken)
	{
		Guard.Against.Default(request.PublicId);

		WorkTime? workTime = await data.WorkTime.FirstOrDefaultAsync(x => x.PublicId == request.PublicId, cancellationToken: cancellationToken);
		Guard.Against.Null(workTime);


		_ = data.WorkTime.Remove(workTime);
		return await data.SaveChangesAsync(cancellationToken) == 1;
	}
}
