using MediatR;

using TaskTimeManage.Core.Models;

namespace TaskTimeManage.Core.Commands.Authentication;
public record RegistrateUserCommand(string Username, string Password) : IRequest<UserModel>;
