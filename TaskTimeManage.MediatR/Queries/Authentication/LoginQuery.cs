using MediatR;

namespace TaskTimeManage.MediatR.Queries.Authentication;

public record LoginQuery(string Username, string Password,string TokenKey) : IRequest<string>;
