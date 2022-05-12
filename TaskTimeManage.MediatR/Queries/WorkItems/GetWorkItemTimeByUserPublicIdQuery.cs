using MediatR;

using TaskTimeManage.Core.Models;

namespace TaskTimeManage.Core.Queries.WorkItems;
public record GetWorkItemTimeByUserPublicIdQuery(Guid PublicId) : IRequest<IEnumerable<WorkItemModel>>;

