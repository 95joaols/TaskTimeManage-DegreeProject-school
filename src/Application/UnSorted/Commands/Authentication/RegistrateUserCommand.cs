using Application.Models;

using MediatR;

namespace Application.Commands.Authentication;
public record RegistrateUserCommand(string Username, string Password) : IRequest<UserModel>;
