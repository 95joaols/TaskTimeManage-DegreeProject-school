using MediatR;

namespace TaskTimeManage.Core.Queries.Authentication;

public record LoginQuery(string Username, string Password, string TokenKey) : IRequest<string>;
