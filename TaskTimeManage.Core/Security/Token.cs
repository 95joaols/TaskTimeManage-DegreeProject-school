using Microsoft.IdentityModel.Tokens;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

using TaskTimeManage.Domain.Entity;

namespace TaskTimeManage.Core.Security;

public class Token
{
	private const string Secret = "GCuwl/Jf8ob5vNDTSsrorORpr81X5FV818rnsvhRfK1KqJ/xobg6M9VCxjVyGGbxnO0LwsI5IjLrbogshFVXTg==";

	public static string GenerateToken(User user, int expireMinutes = 540)
	{
		byte[]? symmetricKey = Convert.FromBase64String(Secret);
		JwtSecurityTokenHandler? tokenHandler = new();

		DateTime now = DateTime.UtcNow;
		SecurityTokenDescriptor? tokenDescriptor = new() {
			Subject = new ClaimsIdentity(new[]
				{
									new Claim("PublicId",user.PublicId.ToString()),
									new Claim(ClaimTypes.Name, user.UserName)
							}),

			Expires = now.AddMinutes(Convert.ToInt32(expireMinutes)),

			SigningCredentials = new SigningCredentials(
						new SymmetricSecurityKey(symmetricKey),
						SecurityAlgorithms.HmacSha256Signature)
		};

		SecurityToken? stoken = tokenHandler.CreateToken(tokenDescriptor);
		string? token = tokenHandler.WriteToken(stoken);

		return token;
	}
}
