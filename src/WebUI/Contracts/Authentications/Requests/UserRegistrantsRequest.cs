namespace WebUI.Contracts.Authentications.Requests;

public record UserRegistrantsRequest(string Username, string Password, string RepeatPassword);
