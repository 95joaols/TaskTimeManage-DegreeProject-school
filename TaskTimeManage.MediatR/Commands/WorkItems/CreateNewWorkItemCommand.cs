using MediatR;

using TaskTimeManage.MediatR.Models;

namespace TaskTimeManage.MediatR.Commands.WorkItems;
public record CreateNewWorkItemCommand(string Name, Guid UserPublicId) : IRequest<WorkItemModel>;

