using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Service;
using Application.CQRS.Authentication.Queries;
using Ardalis.GuardClauses;
using Domain.Aggregates.UserAggregate;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Application.CQRS.Authentication.Handlers;

public class LoginHandler : IRequestHandler<LoginQuery, string>
{
  private readonly IApplicationDbContext _data;
  private readonly IdentityService _identityService;
  private readonly UserManager<IdentityUser> _userManager;

  public LoginHandler(IApplicationDbContext data, UserManager<IdentityUser> userManager,
    IdentityService identityService)
  {
    _data = data;
    _userManager = userManager;
    _identityService = identityService;
  }

  public async Task<string> Handle(LoginQuery request, CancellationToken cancellationToken)
  {
    _ = Guard.Against.NullOrWhiteSpace(request.Username);
    _ = Guard.Against.NullOrWhiteSpace(request.Password);

    IdentityUser identityUser = await ValidateAndGetIdentityAsync(request);

    UserProfile? userProfile = await _data.UserProfile
      .FirstOrDefaultAsync(up => up.IdentityId == new Guid(identityUser.Id), cancellationToken);

    if (userProfile == null)
    {
      throw new LogInWrongException();
    }


    return GetJwtString(identityUser, userProfile);
  }

  private async Task<IdentityUser> ValidateAndGetIdentityAsync(LoginQuery request)
  {
    IdentityUser? identityUser = await _userManager.FindByNameAsync(request.Username);

    if (identityUser is null)
    {
      throw new LogInWrongException();
    }

    bool validPassword = await _userManager.CheckPasswordAsync(identityUser, request.Password);

    if (!validPassword)
    {
      throw new LogInWrongException();
    }

    return identityUser;
  }

  private string GetJwtString(IdentityUser identityUser, UserProfile userProfile)
  {
    ClaimsIdentity claimsIdentity = new(new[] {
      new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
      new Claim(JwtRegisteredClaimNames.UniqueName, userProfile.UserName), new Claim("IdentityId", identityUser.Id),
      new Claim(ClaimTypes.NameIdentifier, userProfile.PublicId.ToString())
    });

    SecurityToken token = _identityService.CreateSecurityToken(claimsIdentity);
    return _identityService.WriteToken(token);
  }
}