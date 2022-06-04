namespace WebUI.Contracts.WorkItems.Requests;

public record EditWorkItemRequest(string Name, IEnumerable<WorkTimeLight>? WorkTimes);