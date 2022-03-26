using Microsoft.IdentityModel.Tokens;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

using TaskTimeManage.Domain.Entity;

namespace TaskTimeManage.Domain.Security
{
    public class Token
    {
        private const string Secret = "GCuwl/Jf8ob5vNDTSsrorORpr81X5FV818rnsvhRfK1KqJ/xobg6M9VCxjVyGGbxnO0LwsI5IjLrbogshFVXTg==";

        public static string GenerateToken(User user, int expireMinutes = 540)
        {
            var symmetricKey = Convert.FromBase64String(Secret);
            var tokenHandler = new JwtSecurityTokenHandler();

            var now = DateTime.UtcNow;
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.UserName)
                }),

                Expires = now.AddMinutes(Convert.ToInt32(expireMinutes)),

                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(symmetricKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var stoken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(stoken);

            return token;
        }
    }
}
