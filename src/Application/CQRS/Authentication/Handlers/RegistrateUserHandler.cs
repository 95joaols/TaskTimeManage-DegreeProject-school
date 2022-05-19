using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Security;
using Application.CQRS.Authentication.Commands;

using Ardalis.GuardClauses;

using Domain.Aggregates.UserAggregate;

using MediatR;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Application.CQRS.Authentication.Handlers;

public class RegistrateUserHandler : IRequestHandler<RegistrateUserCommand, UserProfile>
{
  private readonly IApplicationDbContextWithTransaction _data;
  private readonly UserManager<IdentityUser> _userManager;

  public RegistrateUserHandler(IApplicationDbContextWithTransaction data, UserManager<IdentityUser> userManager)
  {
    _data = data;
    _userManager = userManager;
  }

  public async Task<UserProfile> Handle(RegistrateUserCommand request, CancellationToken cancellationToken)
  {
    _ = Guard.Against.NullOrWhiteSpace(request.Username);
    _ = Guard.Against.NullOrWhiteSpace(request.Password);

    await ValidateIdentityDoesNotExist(request);

    string salt = Cryptography.CreatSalt();
    string hashedPassword =
     Cryptography.Encrypt(Cryptography.Hash(Cryptography.Encrypt(request.Password.Trim(), salt), salt), salt);

    await using var transaction = await _data.Database.BeginTransactionAsync(cancellationToken);

    var identity = await CreateIdentityUserAsync(request, transaction, hashedPassword, cancellationToken);

    var profile = await CreateUserProfileAsync(request, transaction, hashedPassword, identity, cancellationToken);

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
  
    UserProfile createdUser = UserProfile.CreateUser(request.Username, Guid.NewGuid().ToString(), salt);
    return createdUser;
  }
  private async Task ValidateIdentityDoesNotExist(RegistrateUserCommand request)
  {
    var existingIdentity = await _userManager.FindByNameAsync(request.Username);

    if (existingIdentity != null)
      throw new UserAlreadyExistsException();

  }
  private async Task<IdentityUser> CreateIdentityUserAsync(RegistrateUserCommand request, IDbContextTransaction transaction,string hassedPassword, CancellationToken cancellationToken)
  {
    var identity = new IdentityUser { Email = request.Username, UserName = request.Username };
    var createdIdentity = await _userManager.CreateAsync(identity, hassedPassword);
    if (!createdIdentity.Succeeded)
    {
      await transaction.RollbackAsync(cancellationToken);

      foreach (var identityError in createdIdentity.Errors)
      {
        throw new FailToCreateUserException();
      }
    }
    return identity;
  }
  private async Task<UserProfile> CreateUserProfileAsync(RegistrateUserCommand request, IDbContextTransaction transaction, string hassedPassword, IdentityUser identity,
       CancellationToken cancellationToken)
  {
    try
    {
      var profileInfo = BasicInfo.CreateBasicInfo(request.FirstName, request.LastName, request.Username,
          request.Phone, request.DateOfBirth, request.CurrentCity);

      var profile = UserProfile.CreateUserProfile(identity.Id, profileInfo);
      _ctx.UserProfiles.Add(profile);
      await _ctx.SaveChangesAsync(cancellationToken);
      return profile;
    }
    catch (Exception e)
    {
      await transaction.RollbackAsync(cancellationToken);
      throw;
    }
  }
}