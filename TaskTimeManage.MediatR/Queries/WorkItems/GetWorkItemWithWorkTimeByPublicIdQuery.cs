using MediatR;

using TaskTimeManage.Core.Models;

namespace TaskTimeManage.Core.Queries.WorkItems;
public record GetWorkItemWithWorkTimeByPublicIdQuery(Guid PublicId) : IRequest<WorkItemModel?>;


