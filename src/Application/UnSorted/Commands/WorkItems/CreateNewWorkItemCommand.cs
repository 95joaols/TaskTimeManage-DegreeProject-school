using Application.Models;

using MediatR;

namespace Application.Commands.WorkItems;
public record CreateNewWorkItemCommand(string Name, Guid UserPublicId) : IRequest<WorkItemModel>;

