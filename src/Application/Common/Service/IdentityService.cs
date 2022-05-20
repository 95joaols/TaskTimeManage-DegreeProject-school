using Application.Common.Settings;

using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using System.IdentityModel.Tokens.Jwt;
using System.Runtime;
using System.Security.Claims;
using System.Text;

namespace Application.Common.Service;
public class IdentityService
{
  private readonly JwtSettings _jwtSettings;
  private readonly byte[] _key;

  public IdentityService(IOptions<JwtSettings> jwtOptions)
  {
    _jwtSettings = jwtOptions.Value;
    _key = Encoding.ASCII.GetBytes(_jwtSettings.SigningKey);
}
  public IdentityService(JwtSettings jwtSettings)
  {
    _jwtSettings = jwtSettings;
    _key = Encoding.ASCII.GetBytes(_jwtSettings.SigningKey);
  }

  public JwtSecurityTokenHandler TokenHandler = new JwtSecurityTokenHandler();

  public SecurityToken CreateSecurityToken(ClaimsIdentity identity)
  {
    var tokenDescriptor = GetTokenDescriptor(identity);

    return TokenHandler.CreateToken(tokenDescriptor);
  }

  public string WriteToken(SecurityToken token)
  {
    return TokenHandler.WriteToken(token);
  }

  private SecurityTokenDescriptor GetTokenDescriptor(ClaimsIdentity identity)
  {
    return new SecurityTokenDescriptor() {
      Subject = identity,
      Expires = DateTime.Now.AddDays(1),
      Issuer = _jwtSettings.Issuer,
      SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(_key),
            SecurityAlgorithms.HmacSha256Signature)
    };
  }

}
