using MediatR;

namespace Application.CQRS.Authentication.Queries;

public record LoginQuery(string Username, string Password, string TokenKey) : IRequest<string>;
