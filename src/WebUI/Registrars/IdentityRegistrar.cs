using Application.Common.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace WebUI.Registrars;

public class IdentityRegistrar : IWebApplicationBuilderRegistrar
{
  public void RegisterServices(WebApplicationBuilder builder)
  {
    JwtSettings jwtSettings = new();
    builder.Configuration.Bind(nameof(JwtSettings), jwtSettings);

    IConfigurationSection? jwtSection = builder.Configuration.GetSection(nameof(JwtSettings));
    builder.Services.Configure<JwtSettings>(jwtSection);

    builder.Services
      .AddAuthentication(a => {
        a.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        a.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        a.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
      })
      .AddJwtBearer(jwt => {
        jwt.SaveToken = true;
        jwt.TokenValidationParameters = new TokenValidationParameters {
          ValidateIssuerSigningKey = true,
          IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.SigningKey)),
          ValidateIssuer = true,
          ValidIssuer = jwtSettings.Issuer,
          ValidateAudience = false,
          RequireExpirationTime = false,
          ValidateLifetime = true
        };
        jwt.ClaimsIssuer = jwtSettings.Issuer;
      });
  }
}