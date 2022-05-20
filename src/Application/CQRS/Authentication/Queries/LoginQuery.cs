namespace Application.CQRS.Authentication.Queries;

public record LoginQuery(string Username, string Password) : IRequest<string>;