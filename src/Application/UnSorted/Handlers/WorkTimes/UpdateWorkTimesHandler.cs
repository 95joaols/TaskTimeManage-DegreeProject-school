using Application.Commands.WorkTimes;
using Application.DataAccess;
using Application.Models;

using Ardalis.GuardClauses;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Application.Handlers.WorkTimes;
public class UpdateWorkTimesHandler : IRequestHandler<UpdateWorkTimesCommand, IEnumerable<WorkTimeModel>>
{
	private readonly TTMDataAccess data;

	public UpdateWorkTimesHandler(TTMDataAccess data) => this.data = data;
	public async Task<IEnumerable<WorkTimeModel>> Handle(UpdateWorkTimesCommand request, CancellationToken cancellationToken)
	{
		Guard.Against.NullOrEmpty(request.WorkTimes);

		IEnumerable<WorkTimeModel> workTimeModels = await data.WorkTime.Where(wt => request.WorkTimes.Select(x => x.PublicId).Contains(wt.PublicId)).ToListAsync(cancellationToken);

		foreach (WorkTimeModel? workTime in workTimeModels)
		{
			workTime.Time = request.WorkTimes.FirstOrDefault(wt => wt.PublicId == workTime.PublicId).time;
			workTime.Time = workTime.Time > DateTime.UtcNow ? DateTime.UtcNow : workTime.Time;
		}
		_ = await data.SaveChangesAsync(cancellationToken);
		return workTimeModels;
	}
}
