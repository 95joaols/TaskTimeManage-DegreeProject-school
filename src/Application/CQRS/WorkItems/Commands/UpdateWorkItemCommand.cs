using Domain.Aggregates.WorkAggregate;
using MediatR;

namespace Application.CQRS.WorkItems.Commands;

public record UpdateWorkItemCommand(Guid PublicId, string Name) : IRequest<WorkItem>;