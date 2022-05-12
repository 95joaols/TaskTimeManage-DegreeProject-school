using MediatR;

using TaskTimeManage.MediatR.Models;

namespace TaskTimeManage.MediatR.Commands.WorkTimes;
public record CreateWorkTimeCommand(DateTime Time, Guid WorkItemPublicId) : IRequest<WorkTimeModel>;

