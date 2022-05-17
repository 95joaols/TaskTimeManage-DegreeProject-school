using Domain.Aggregates.WorkAggregate;

namespace WebUI.Contracts.WorkItems.Requests;

public record EditWorkItemRequest(Guid PublicId, string Name, IEnumerable<WorkTimeLightRequest>? WorkTimes);

