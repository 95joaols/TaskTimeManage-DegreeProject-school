using MediatR;

namespace TaskTimeManage.Core.Commands.WorkTimes;
public record DeleteAllWorkTimesByWorkItemIdCommand(int WorkItemId) : IRequest<bool>;

