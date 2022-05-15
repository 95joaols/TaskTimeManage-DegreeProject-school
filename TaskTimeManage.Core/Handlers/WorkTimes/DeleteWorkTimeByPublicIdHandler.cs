using MediatR;

using Microsoft.EntityFrameworkCore;

using TaskTimeManage.Core.Commands.WorkTimes;
using TaskTimeManage.Core.DataAccess;
using TaskTimeManage.Core.Models;

namespace TaskTimeManage.Core.Handlers.WorkTimes;
public class DeleteWorkTimeByPublicIdHandler : IRequestHandler<DeleteWorkTimeByPublicIdCommand, bool>
{

	private readonly TTMDataAccess data;

	public DeleteWorkTimeByPublicIdHandler(TTMDataAccess data) => this.data = data;

	public async Task<bool> Handle(DeleteWorkTimeByPublicIdCommand request, CancellationToken cancellationToken)
	{
		WorkTimeModel? workTimeModel = await data.WorkTime.FirstOrDefaultAsync(x => x.PublicId == request.PublicId, cancellationToken: cancellationToken);

		if (workTimeModel == null)
		{
			throw new ArgumentException(nameof(workTimeModel));
		}

		_ = data.WorkTime.Remove(workTimeModel);
		return await data.SaveChangesAsync(cancellationToken) == 1;
	}
}
