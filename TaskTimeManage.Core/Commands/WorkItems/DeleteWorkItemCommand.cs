using MediatR;

namespace TaskTimeManage.Core.Commands.WorkItems;
public record DeleteWorkItemCommand(Guid PublicId) : IRequest<bool>;

