using Application.Common.Models.Generated;

namespace WebUI.Contracts.WorkItems.Requests;

public record EditWorkItemRequest(Guid PublicId, string Name, IEnumerable<WorkTimeDto>? WorkTimes);

