namespace WebUI.Contracts.WorkItems.Requests;

public record WorkTimeLightRequest(Guid PublicId, DateTimeOffset Time);