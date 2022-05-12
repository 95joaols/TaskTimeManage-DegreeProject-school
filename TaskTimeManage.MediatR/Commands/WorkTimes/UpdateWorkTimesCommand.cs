using MediatR;

using TaskTimeManage.MediatR.Models;

namespace TaskTimeManage.MediatR.Commands.WorkTimes;
public record UpdateWorkTimesCommand(IEnumerable<WorkTimeModel> WorkTimes) : IRequest<IEnumerable<WorkTimeModel>>;
