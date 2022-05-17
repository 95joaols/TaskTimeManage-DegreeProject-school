using MediatR;

namespace Application.CQRS.WorkTimes.Commands;

public record DeleteAllWorkTimesByWorkItemIdCommand(int WorkItemId) : IRequest<bool>;