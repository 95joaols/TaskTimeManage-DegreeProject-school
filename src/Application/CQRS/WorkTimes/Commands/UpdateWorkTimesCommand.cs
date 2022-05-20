namespace Application.CQRS.WorkTimes.Commands;

public record UpdateWorkTimesCommand(IEnumerable<WorkTime> WorkTimes) : IRequest<IEnumerable<WorkTime>>;