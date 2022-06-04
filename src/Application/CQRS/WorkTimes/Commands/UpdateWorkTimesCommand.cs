namespace Application.CQRS.WorkTimes.Commands;

public record UpdateWorkTimesCommand(Guid WorkItemPublicId,IEnumerable<WorkTime> WorkTimes) : IRequest<IEnumerable<WorkTime>>;