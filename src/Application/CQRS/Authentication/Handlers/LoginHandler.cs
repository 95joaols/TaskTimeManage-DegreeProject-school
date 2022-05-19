using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Security;
using Application.CQRS.Authentication.Queries;
using Ardalis.GuardClauses;
using Domain.Aggregates.UserAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Authentication.Handlers;

public class LoginHandler : IRequestHandler<LoginQuery, string>
{
  private readonly IApplicationDbContext _data;

  public LoginHandler(IApplicationDbContext data) => _data = data;

  public async Task<string> Handle(LoginQuery request, CancellationToken cancellationToken)
  {
    _ = Guard.Against.NullOrWhiteSpace(request.Username);
    _ = Guard.Against.NullOrWhiteSpace(request.Password);

    UserProfile? user =
      await _data.UserProfile.FirstOrDefaultAsync(u => u.UserName.ToLower() == request.Username.Trim().ToLower(),
        cancellationToken);

    if (user == null)
    {
      throw new LogInWrongException();
    }

    string hashedPassword =
      Cryptography.Encrypt(Cryptography.Hash(Cryptography.Encrypt(request.Password.Trim(), user.Salt), user.Salt),
        user.Salt);
    if (hashedPassword != user.HashedPassword)
    {
      throw new LogInWrongException();
    }

    return Token.GenerateToken(user, request.SiningKey, request.Issuer);
  }
}