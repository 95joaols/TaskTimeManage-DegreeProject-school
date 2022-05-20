namespace Application.CQRS.Authentication.Queries;

public record LoginQuery(string Username, string Password, string SiningKey, string Issuer) : IRequest<string>;