namespace Application.CQRS.WorkItems.Queries;

public record GetWorkItemWithWorkTimeByPublicIdQuery(Guid PublicId) : IRequest<WorkItem?>;