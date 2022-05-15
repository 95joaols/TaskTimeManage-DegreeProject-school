using MediatR;

namespace Application.Queries.Authentication;

public record LoginQuery(string Username, string Password, string TokenKey) : IRequest<string>;
