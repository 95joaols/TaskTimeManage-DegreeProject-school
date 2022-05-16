using Domain.Entities;

using Microsoft.IdentityModel.Tokens;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Application.Common.Security;

public class Token
{
	public static string GenerateToken(User user, string tokenkey)
	{
		SymmetricSecurityKey? key = new(System.Text.Encoding.UTF8.GetBytes(
								tokenkey));
		JwtSecurityTokenHandler? tokenHandler = new();

		_ = DateTime.UtcNow;
		SecurityTokenDescriptor? tokenDescriptor = new() {
			Subject = new ClaimsIdentity(new[]
				{
									new Claim(ClaimTypes.NameIdentifier ,user.PublicId.ToString()),
									new Claim(ClaimTypes.Name, user.UserName)
							}),

			Expires = DateTime.Now.AddDays(1),

			SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
		};

		SecurityToken? stoken = tokenHandler.CreateToken(tokenDescriptor);
		string? token = tokenHandler.WriteToken(stoken);

		return token;
	}
}
