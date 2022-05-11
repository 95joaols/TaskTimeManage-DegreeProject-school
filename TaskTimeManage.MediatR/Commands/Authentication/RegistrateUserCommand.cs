using MediatR;

using TaskTimeManage.MediatR.Models;

namespace TaskTimeManage.MediatR.Commands.Authentication;
public record RegistrateUserCommand(string Username, string Password) : IRequest<UserModel>;
