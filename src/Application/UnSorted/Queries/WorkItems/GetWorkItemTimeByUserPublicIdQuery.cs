using Application.Models;

using MediatR;

namespace Application.Queries.WorkItems;
public record GetWorkItemTimeByUserPublicIdQuery(Guid PublicId) : IRequest<IEnumerable<WorkItemModel>>;

