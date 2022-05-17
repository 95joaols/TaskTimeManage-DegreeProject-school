using Domain.Aggregates.UserAggregate;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Common.Security;

public static class Token
{
  public static string GenerateToken(UserProfile user, string tokenKey)
  {
    SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(
      tokenKey));
    JwtSecurityTokenHandler tokenHandler = new();

    _ = DateTime.UtcNow;
    SecurityTokenDescriptor tokenDescriptor = new() {
      Subject =
        new ClaimsIdentity(new[] {
          new Claim(ClaimTypes.NameIdentifier, user.PublicId.ToString()), new Claim(ClaimTypes.Name, user.UserName)
        }),
      Expires = DateTime.Now.AddDays(1),
      SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
    };

    SecurityToken? securityToken = tokenHandler.CreateToken(tokenDescriptor);
    string? token = tokenHandler.WriteToken(securityToken);

    return token;
  }
}