using MediatR;

using TaskTimeManage.MediatR.Models;

namespace TaskTimeManage.MediatR.Queries.WorkItems;
public record GetWorkItemTimeByUserPublicIdQuery(Guid PublicId) : IRequest<IEnumerable<WorkItemModel>>;

