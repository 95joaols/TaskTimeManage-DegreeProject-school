using MediatR;

using TaskTimeManage.MediatR.Models;

namespace TaskTimeManage.MediatR.Commands.WorkTimes;
public record InsertWorkTimeCommand(WorkTimeModel WorkTimes, Guid WorkItemPublicId) : IRequest<WorkTimeModel>;

