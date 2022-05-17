using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Security;
using Application.CQRS.Authentication.Commands;

using Ardalis.GuardClauses;

using Domain.Entities;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Authentication.Handlers;
public class RegistrateUserHandler : IRequestHandler<RegistrateUserCommand, User>
{
  private readonly IApplicationDbContext data;

  public RegistrateUserHandler(IApplicationDbContext data) => this.data = data;

  public async Task<User> Handle(RegistrateUserCommand request, CancellationToken cancellationToken)
  {
    _ = Guard.Against.NullOrWhiteSpace(request.Username);
    _ = Guard.Against.NullOrWhiteSpace(request.Password);
    User? user = await data.User.FirstOrDefaultAsync(u => u.UserName.ToLower() == request.Username.Trim().ToLower(), cancellationToken);
    if (user != null)
    {
      throw new UserAlreadyExistsException();
    }


    User createdUser = CreateUser(request);
    _ = await data.User.AddAsync(createdUser, cancellationToken);
    _ = await data.SaveChangesAsync(cancellationToken);
    return createdUser;

  }

  private static User CreateUser(RegistrateUserCommand request)
  {
    string salt = Cryptography.CreatSalt();
    string hashedPassword = Cryptography.Encrypt(Cryptography.Hash(Cryptography.Encrypt(request.Password.Trim(), salt), salt), salt);
    User createdUser = new() {
      UserName = request.Username,
      Password = hashedPassword,
      Salt = salt,
    };
    return createdUser;
  }
}
