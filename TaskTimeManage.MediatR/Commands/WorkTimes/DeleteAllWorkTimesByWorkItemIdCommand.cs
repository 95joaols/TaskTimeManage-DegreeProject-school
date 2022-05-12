using MediatR;

namespace TaskTimeManage.MediatR.Commands.WorkTimes;
public record DeleteAllWorkTimesByWorkItemIdCommand(int WorkItemId) : IRequest<bool>;

