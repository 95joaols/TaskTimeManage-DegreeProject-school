using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace WebUI.Options;

public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
{
  public void Configure(SwaggerGenOptions options)
  {
    OpenApiSecurityScheme scheme = GetJwtSecurityScheme();
    options.AddSecurityDefinition(scheme.Reference.Id, scheme);
    options.AddSecurityRequirement(new OpenApiSecurityRequirement { { scheme, new string[0] } });
  }

  private OpenApiInfo CreateVersionInfo()
  {
    OpenApiInfo info = new() { Title = "CwkSocial", Version = "V1" };

    return info;
  }

  private OpenApiSecurityScheme GetJwtSecurityScheme() =>
    new() {
      Name = "JWT Authentication",
      Description = "Provide a JWT Bearer",
      In = ParameterLocation.Header,
      Type = SecuritySchemeType.Http,
      Scheme = "bearer",
      BearerFormat = "JWT",
      Reference = new OpenApiReference {
        Id = JwtBearerDefaults.AuthenticationScheme, Type = ReferenceType.SecurityScheme
      }
    };
}