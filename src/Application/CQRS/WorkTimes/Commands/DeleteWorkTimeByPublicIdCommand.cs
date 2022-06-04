namespace Application.CQRS.WorkTimes.Commands;

public record DeleteWorkTimeByPublicIdCommand(Guid WorkItemPublicId, Guid PublicId) : IRequest<bool>;