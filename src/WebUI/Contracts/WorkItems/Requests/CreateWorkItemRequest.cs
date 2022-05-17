namespace WebUI.Contracts.WorkItems.Requests;

public record CreateWorkItemRequest(string Name, Guid UserPublicId);