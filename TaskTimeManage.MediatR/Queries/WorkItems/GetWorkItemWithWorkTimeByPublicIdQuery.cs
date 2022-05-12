using MediatR;

using TaskTimeManage.MediatR.Models;

namespace TaskTimeManage.MediatR.Queries.WorkItems;
public record GetWorkItemWithWorkTimeByPublicIdQuery(Guid PublicId) : IRequest<WorkItemModel?>;


