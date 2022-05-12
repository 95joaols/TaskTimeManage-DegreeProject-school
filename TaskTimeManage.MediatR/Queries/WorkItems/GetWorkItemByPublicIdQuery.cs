using MediatR;

using TaskTimeManage.MediatR.Models;

namespace TaskTimeManage.MediatR.Queries.WorkItems;
public record GetWorkItemByPublicIdQuery(Guid PublicId) : IRequest<WorkItemModel?>;


