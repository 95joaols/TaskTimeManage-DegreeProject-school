using Domain.Entities;

using MediatR;

namespace Application.CQRS.WorkItems.Queries;
public record GetWorkItemTimeByUserPublicIdQuery(Guid PublicId) : IRequest<IEnumerable<WorkItem>>;

