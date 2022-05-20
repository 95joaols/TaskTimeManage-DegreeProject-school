using Application.Common.Service;
using Microsoft.AspNetCore.Identity;

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
    Guard.Against.NullOrWhiteSpace(request.Username);
    Guard.Against.NullOrWhiteSpace(request.Password);

    var identityUser = await ValidateAndGetIdentityAsync(request);

    var userProfile = await _data.UserProfile
      .FirstOrDefaultAsync(up => up.IdentityId == new Guid(identityUser.Id), cancellationToken);

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
      }
    );

    var token = _identityService.CreateSecurityToken(claimsIdentity);

    return _identityService.WriteToken(token);
  }
}