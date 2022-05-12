using MediatR;

using TaskTimeManage.MediatR.Models;

namespace TaskTimeManage.MediatR.Commands.WorkItems;
public record UpdateWorkItemCommand(Guid PublicId, string Name) : IRequest<WorkItemModel>;

