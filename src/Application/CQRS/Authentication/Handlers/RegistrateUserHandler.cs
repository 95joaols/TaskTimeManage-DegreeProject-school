using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.CQRS.Authentication.Commands;
using Ardalis.GuardClauses;
using Domain.Aggregates.UserAggregate;
using MediatR;
using Microsoft.AspNetCore.Identity;
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


    await using IDbContextTransaction transaction = await _data.CreateTransactionAsync(cancellationToken);
    UserProfile createdUser;
    try
    {
      IdentityUser identity = await CreateIdentityUserAsync(request, transaction, request.Password, cancellationToken);
      createdUser = await CreateUserAsync(request, transaction, identity, cancellationToken);
      await transaction.CommitAsync(cancellationToken);
    }
    catch (Exception ex)
    {
      await transaction.RollbackAsync(cancellationToken);
      throw;
    }

    return createdUser;
  }

  private async Task<UserProfile> CreateUserAsync(RegistrateUserCommand request, IDbContextTransaction transaction,
    IdentityUser identity,
    CancellationToken cancellationToken)
  {
    try
    {
      UserProfile createdUser = UserProfile.CreateUser(request.Username, new Guid(identity.Id));
      _ = await _data.UserProfile.AddAsync(createdUser, cancellationToken);
      await _data.SaveChangesAsync(cancellationToken);

      return createdUser;
    }
    catch (Exception e)
    {
      await transaction.RollbackAsync(cancellationToken);
      throw;
    }
  }

  private async Task ValidateIdentityDoesNotExist(RegistrateUserCommand request)
  {
    IdentityUser? existingIdentity = await _userManager.FindByNameAsync(request.Username);

    if (existingIdentity != null)
    {
      throw new UserAlreadyExistsException();
    }
  }

  private async Task<IdentityUser> CreateIdentityUserAsync(RegistrateUserCommand request,
    IDbContextTransaction transaction, string hassedPassword, CancellationToken cancellationToken)
  {
    IdentityUser identity = new() { Email = request.Username, UserName = request.Username };
    IdentityResult? createdIdentity = await _userManager.CreateAsync(identity, hassedPassword);
    if (!createdIdentity.Succeeded)
    {
      await transaction.RollbackAsync(cancellationToken);

      foreach (IdentityError? identityError in createdIdentity.Errors)
      {
        throw new FailToCreateUserException();
      }
    }

    return identity;
  }
}