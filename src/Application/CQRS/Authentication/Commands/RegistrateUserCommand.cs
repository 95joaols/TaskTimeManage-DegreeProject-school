
using Domain.Entities;

using MediatR;

namespace Application.CQRS.Authentication.Commands;
public record RegistrateUserCommand(string Username, string Password) : IRequest<User>;
