using MediatR;

using Microsoft.EntityFrameworkCore;

using TaskTimeManage.MediatR.Commands.WorkTimes;
using TaskTimeManage.MediatR.DataAccess;
using TaskTimeManage.MediatR.Models;

namespace TaskTimeManage.MediatR.Handlers.WorkTimes;
public class UpdateWorkTimesHandler : IRequestHandler<UpdateWorkTimesCommand, IEnumerable<WorkTimeModel>>
{
	private readonly TTMDataAccess data;

	public UpdateWorkTimesHandler(TTMDataAccess data) => this.data = data;
	public async Task<IEnumerable<WorkTimeModel>> Handle(UpdateWorkTimesCommand request, CancellationToken cancellationToken)
	{
		if (request.WorkTimes == null)
		{
			throw new ArgumentNullException(nameof(request.WorkTimes));
		}
		if (!request.WorkTimes.Any())
		{
			throw new ArgumentException(nameof(request.WorkTimes));
		}

		IEnumerable<WorkTimeModel> workTimeModels = await data.WorkTime.Where(wt => request.WorkTimes.Select(x => x.PublicId).Contains(wt.PublicId)).ToListAsync(cancellationToken: cancellationToken);

		foreach (var workTime in workTimeModels)
		{
			workTime.Time = request.WorkTimes.FirstOrDefault(wt => wt.PublicId == workTime.PublicId)?.Time ?? workTime.Time;
		}
		await data.SaveChangesAsync(cancellationToken);
		return workTimeModels;
	}
}
