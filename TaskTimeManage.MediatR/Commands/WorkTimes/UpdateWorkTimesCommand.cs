using MediatR;

using TaskTimeManage.Core.Dto;
using TaskTimeManage.Core.Models;

namespace TaskTimeManage.Core.Commands.WorkTimes;
public record UpdateWorkTimesCommand(IEnumerable<WorkTimesLight> WorkTimes) : IRequest<IEnumerable<WorkTimeModel>>;
