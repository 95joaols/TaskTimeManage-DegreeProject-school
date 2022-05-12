using MediatR;

using TaskTimeManage.Core.Models;

namespace TaskTimeManage.Core.Queries.WorkTimes;
public record GetWorkTimesByWorkItemPublicIdQuery(Guid PublicId) : IRequest<IEnumerable<WorkTimeModel>>;

