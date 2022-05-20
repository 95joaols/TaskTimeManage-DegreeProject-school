using Application.CQRS.Authentication.Commands;
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
    Guard.Against.NullOrWhiteSpace(request.Username);
    Guard.Against.NullOrWhiteSpace(request.Password);

    await ValidateIdentityDoesNotExist(request);


    await using var transaction = await _data.CreateTransactionAsync(cancellationToken);
    UserProfile createdUser;
    try
    {
      var identity = await CreateIdentityUserAsync(request, transaction, request.Password, cancellationToken);
      createdUser = await CreateUserAsync(request, transaction, identity, cancellationToken);
      await transaction.CommitAsync(cancellationToken);
    }
    catch (Exception)
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
      var createdUser = UserProfile.CreateUser(request.Username, new Guid(identity.Id));
      await _data.UserProfile.AddAsync(createdUser, cancellationToken);
      await _data.SaveChangesAsync(cancellationToken);

      return createdUser;
    }
    catch (Exception)
    {
      await transaction.RollbackAsync(cancellationToken);

      throw;
    }
  }

  private async Task ValidateIdentityDoesNotExist(RegistrateUserCommand request)
  {
    var existingIdentity = await _userManager.FindByNameAsync(request.Username);

    if (existingIdentity != null)
    {
      throw new UserAlreadyExistsException();
    }
  }

  private async Task<IdentityUser> CreateIdentityUserAsync(RegistrateUserCommand request,
    IDbContextTransaction transaction, string hassedPassword, CancellationToken cancellationToken)
  {
    IdentityUser identity = new() {
      Email = request.Username,
      UserName = request.Username
    };
    var createdIdentity = await _userManager.CreateAsync(identity, hassedPassword);
    if (createdIdentity.Succeeded)
    {
      return identity;
    }

    await transaction.RollbackAsync(cancellationToken);

    throw new FailToCreateUserException();
  }
}