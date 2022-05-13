using MediatR;

using Microsoft.EntityFrameworkCore;

using TaskTimeManage.Core.Commands.WorkTimes;
using TaskTimeManage.Core.DataAccess;
using TaskTimeManage.Core.Models;

namespace TaskTimeManage.Core.Handlers.WorkTimes;
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

		IEnumerable<WorkTimeModel> workTimeModels = await data.WorkTime.Where(wt => request.WorkTimes.Select(x => x.PublicId).Contains(wt.PublicId)).ToListAsync(cancellationToken);

		foreach (var workTime in workTimeModels)
		{
			workTime.Time = request.WorkTimes.FirstOrDefault(wt => wt.PublicId == workTime.PublicId).time;
			workTime.Time = workTime.Time > DateTime.UtcNow ?   DateTime.UtcNow : workTime.Time;
		}
		await data.SaveChangesAsync(cancellationToken);
		return workTimeModels;
	}
}
