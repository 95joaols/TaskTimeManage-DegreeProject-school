using MediatR;

using TaskTimeManage.MediatR.Models;

namespace TaskTimeManage.MediatR.Commands.WorkItems;
public record UpdateWorkItemCommand(WorkItemModel WorkItem) : IRequest<WorkItemModel>;

