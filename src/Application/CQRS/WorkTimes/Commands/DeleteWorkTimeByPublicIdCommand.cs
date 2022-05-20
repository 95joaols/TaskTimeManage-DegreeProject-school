namespace Application.CQRS.WorkTimes.Commands;

public record DeleteWorkTimeByPublicIdCommand(Guid PublicId) : IRequest<bool>;