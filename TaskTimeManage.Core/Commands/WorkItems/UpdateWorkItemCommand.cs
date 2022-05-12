using MediatR;

using TaskTimeManage.Core.Models;

namespace TaskTimeManage.Core.Commands.WorkItems;
public record UpdateWorkItemCommand(Guid PublicId, string Name) : IRequest<WorkItemModel>;

