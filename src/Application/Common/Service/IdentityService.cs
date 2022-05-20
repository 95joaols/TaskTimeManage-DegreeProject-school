namespace Application.Common.Service;

public class IdentityService
{
  private readonly JwtSettings _jwtSettings;
  private readonly byte[] _key;

  private readonly JwtSecurityTokenHandler _tokenHandler = new();

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

  public SecurityToken CreateSecurityToken(ClaimsIdentity identity)
  {
    var tokenDescriptor = GetTokenDescriptor(identity);

    return _tokenHandler.CreateToken(tokenDescriptor);
  }

  public string WriteToken(SecurityToken token) => _tokenHandler.WriteToken(token);

  private SecurityTokenDescriptor GetTokenDescriptor(ClaimsIdentity identity) =>
    new() {
      Subject = identity,
      Expires = DateTime.Now.AddDays(1),
      Issuer = _jwtSettings.Issuer,
      SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(_key),
        SecurityAlgorithms.HmacSha256Signature
      )
    };
}