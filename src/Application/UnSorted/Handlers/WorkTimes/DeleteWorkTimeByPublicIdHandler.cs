using Application.Commands.WorkTimes;
using Application.DataAccess;
using Application.Models;

using Ardalis.GuardClauses;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Application.Handlers.WorkTimes;
public class DeleteWorkTimeByPublicIdHandler : IRequestHandler<DeleteWorkTimeByPublicIdCommand, bool>
{

	private readonly TTMDataAccess data;

	public DeleteWorkTimeByPublicIdHandler(TTMDataAccess data) => this.data = data;

	public async Task<bool> Handle(DeleteWorkTimeByPublicIdCommand request, CancellationToken cancellationToken)
	{
		Guard.Against.Default(request.PublicId);

		WorkTimeModel? workTimeModel = await data.WorkTime.FirstOrDefaultAsync(x => x.PublicId == request.PublicId, cancellationToken: cancellationToken);
		Guard.Against.Null(workTimeModel);


		_ = data.WorkTime.Remove(workTimeModel);
		return await data.SaveChangesAsync(cancellationToken) == 1;
	}
}
