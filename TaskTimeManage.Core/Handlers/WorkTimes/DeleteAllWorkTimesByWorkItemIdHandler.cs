using MediatR;

using TaskTimeManage.Core.Commands.WorkTimes;
using TaskTimeManage.Core.DataAccess;

namespace TaskTimeManage.Core.Handlers.WorkTimes;
public class DeleteAllWorkTimesByWorkItemIdHandler : IRequestHandler<DeleteAllWorkTimesByWorkItemIdCommand, bool>
{

	private readonly TTMDataAccess data;

	public DeleteAllWorkTimesByWorkItemIdHandler(TTMDataAccess data) => this.data = data;

	public async Task<bool> Handle(DeleteAllWorkTimesByWorkItemIdCommand request, CancellationToken cancellationToken)
	{
		if (request.WorkItemId == 0)
		{
			throw new ArgumentOutOfRangeException(nameof(request.WorkItemId));
		}

		data.WorkTime.RemoveRange(data.WorkTime.Where(wt => wt.WorkItemId == request.WorkItemId));

		_ = await data.SaveChangesAsync(cancellationToken);
		return true;
	}
}
