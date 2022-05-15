using Application.Models;

using MediatR;

namespace Application.Queries.WorkItems;
public record GetWorkItemWithWorkTimeByPublicIdQuery(Guid PublicId) : IRequest<WorkItemModel?>;


