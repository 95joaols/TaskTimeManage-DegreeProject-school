namespace Application.CQRS.WorkItems.Commands;

public record DeleteWorkItemCommand(Guid PublicId) : IRequest<bool>;