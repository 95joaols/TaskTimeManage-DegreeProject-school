using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Service;
using Application.CQRS.Authentication.Queries;

using Ardalis.GuardClauses;

using Domain.Aggregates.UserAggregate;

using MediatR;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Application.CQRS.Authentication.Handlers;

public class LoginHandler : IRequestHandler<LoginQuery, string>
{
  private readonly IApplicationDbContext _data;
  private readonly UserManager<IdentityUser> _userManager;
  private readonly IdentityService _identityService;

  public LoginHandler(IApplicationDbContext data, UserManager<IdentityUser> userManager, IdentityService identityService)
  {
    _data = data;
    _userManager = userManager;
    _identityService = identityService;
  }

  public async Task<string> Handle(LoginQuery request, CancellationToken cancellationToken)
  {
    _ = Guard.Against.NullOrWhiteSpace(request.Username);
    _ = Guard.Against.NullOrWhiteSpace(request.Password);

    var identityUser = await ValidateAndGetIdentityAsync(request);

    var userProfile = await _data.UserProfile
                .FirstOrDefaultAsync(up => up.IdentityId == new Guid(identityUser.Id), cancellationToken:
                    cancellationToken);

    if (userProfile == null)
    {
      throw new LogInWrongException();
    }



    return GetJwtString(identityUser, userProfile);
  }

  private async Task<IdentityUser> ValidateAndGetIdentityAsync(LoginQuery request)
  {
    var identityUser = await _userManager.FindByNameAsync(request.Username);

    if (identityUser is null)
      throw new LogInWrongException();

    var validPassword = await _userManager.CheckPasswordAsync(identityUser, request.Password);

    if (!validPassword)
      throw new LogInWrongException();

    return identityUser;
  }

  private string GetJwtString(IdentityUser identityUser, UserProfile userProfile)
  {
    var claimsIdentity = new ClaimsIdentity(new Claim[]
    {
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.UniqueName, userProfile.UserName),
            new Claim("IdentityId", identityUser.Id.ToString()),
            new Claim(ClaimTypes.NameIdentifier, userProfile.PublicId.ToString())
    });

    var token = _identityService.CreateSecurityToken(claimsIdentity);
    return _identityService.WriteToken(token);
  }
}