namespace Application.CQRS.Authentication.Commands;

public record RegistrateUserCommand(string Username, string Password) : IRequest<UserProfile>;