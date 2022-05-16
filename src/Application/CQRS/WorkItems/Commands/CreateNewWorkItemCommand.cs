using Domain.Entities;

using MediatR;

namespace Application.CQRS.WorkItems.Commands;
public record CreateNewWorkItemCommand(string Name, Guid UserPublicId) : IRequest<WorkItem>;

