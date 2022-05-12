using MediatR;

using TaskTimeManage.MediatR.Commands.WorkTimes;
using TaskTimeManage.MediatR.DataAccess;

namespace TaskTimeManage.MediatR.Handlers.WorkTimes;
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

		await data.SaveChangesAsync(cancellationToken);
		return true;
	}
}
