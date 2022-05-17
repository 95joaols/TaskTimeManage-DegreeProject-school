﻿using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Security;
using Application.CQRS.Authentication.Commands;
using Ardalis.GuardClauses;
using Domain.Aggregates.UserAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Authentication.Handlers;

public class RegistrateUserHandler : IRequestHandler<RegistrateUserCommand, UserProfile>
{
  private readonly IApplicationDbContext _data;

  public RegistrateUserHandler(IApplicationDbContext data) => _data = data;

  public async Task<UserProfile> Handle(RegistrateUserCommand request, CancellationToken cancellationToken)
  {
    _ = Guard.Against.NullOrWhiteSpace(request.Username);
    _ = Guard.Against.NullOrWhiteSpace(request.Password);
    UserProfile? user =
      await _data.UserProfile.FirstOrDefaultAsync(u => u.UserName.ToLower() == request.Username.Trim().ToLower(),
        cancellationToken);
    if (user != null)
    {
      throw new UserAlreadyExistsException();
    }


    UserProfile createdUser = CreateUser(request);
    _ = await _data.UserProfile.AddAsync(createdUser, cancellationToken);
    _ = await _data.SaveChangesAsync(cancellationToken);
    return createdUser;
  }

  private static UserProfile CreateUser(RegistrateUserCommand request)
  {
    string salt = Cryptography.CreatSalt();
    string hashedPassword =
      Cryptography.Encrypt(Cryptography.Hash(Cryptography.Encrypt(request.Password.Trim(), salt), salt), salt);
    UserProfile createdUser = UserProfile.CreateUser(request.Username, Guid.NewGuid().ToString(), hashedPassword, salt);
    return createdUser;
  }
}