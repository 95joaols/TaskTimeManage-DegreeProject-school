using MediatR;

using TaskTimeManage.MediatR.Models;

namespace TaskTimeManage.MediatR.Queries.WorkTimes;
public record GetWorkTimesByWorkItemPublicIdQuery(Guid PublicId) : IRequest<IEnumerable<WorkTimeModel>>;

