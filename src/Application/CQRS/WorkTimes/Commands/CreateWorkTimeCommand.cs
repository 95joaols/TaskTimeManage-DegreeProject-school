namespace Application.CQRS.WorkTimes.Commands;

public record CreateWorkTimeCommand(DateTimeOffset Time, Guid WorkItemPublicId) : IRequest<WorkTime>;