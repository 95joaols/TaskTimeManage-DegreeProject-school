using MediatR;

namespace Application.Commands.WorkItems;
public record DeleteWorkItemCommand(Guid PublicId) : IRequest<bool>;

