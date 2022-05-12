using MediatR;

using Microsoft.EntityFrameworkCore;

using TaskTimeManage.MediatR.Commands.WorkTimes;
using TaskTimeManage.MediatR.DataAccess;
using TaskTimeManage.MediatR.Models;

namespace TaskTimeManage.MediatR.Handlers.WorkTimes;
public class DeleteWorkTimeByPublicIdHandler : IRequestHandler<DeleteWorkTimeByPublicIdCommand, bool>
{

	private readonly TTMDataAccess data;

	public DeleteWorkTimeByPublicIdHandler(TTMDataAccess data) => this.data = data;

	public async Task<bool> Handle(DeleteWorkTimeByPublicIdCommand request, CancellationToken cancellationToken)
	{
		WorkTimeModel? workTimeModel = await data.WorkTime.FirstOrDefaultAsync(x => x.PublicId == request.PublicId, cancellationToken: cancellationToken);

		if (workTimeModel == null)
			throw new ArgumentException(nameof(workTimeModel));

		data.WorkTime.Remove(workTimeModel);
		return await data.SaveChangesAsync(cancellationToken) == 1;
	}
}
