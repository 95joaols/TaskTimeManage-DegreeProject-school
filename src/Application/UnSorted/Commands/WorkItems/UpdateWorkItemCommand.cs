using Application.Models;

using MediatR;

namespace Application.Commands.WorkItems;
public record UpdateWorkItemCommand(Guid PublicId, string Name) : IRequest<WorkItemModel>;

