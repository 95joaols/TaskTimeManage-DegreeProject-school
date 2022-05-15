using MediatR;

namespace Application.Commands.WorkTimes;
public record DeleteAllWorkTimesByWorkItemIdCommand(int WorkItemId) : IRequest<bool>;

