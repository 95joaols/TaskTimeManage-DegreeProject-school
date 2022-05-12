using MediatR;

using TaskTimeManage.Core.Models;

namespace TaskTimeManage.Core.Commands.WorkItems;
public record CreateNewWorkItemCommand(string Name, Guid UserPublicId) : IRequest<WorkItemModel>;

