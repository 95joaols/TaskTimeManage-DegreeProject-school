using MediatR;

using TaskTimeManage.Core.Models;

namespace TaskTimeManage.Core.Commands.WorkTimes;
public record CreateWorkTimeCommand(DateTime Time, Guid WorkItemPublicId) : IRequest<WorkTimeModel>;

