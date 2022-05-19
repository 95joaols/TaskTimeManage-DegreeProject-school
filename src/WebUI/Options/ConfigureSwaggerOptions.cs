using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace WebUI.Options;

public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
{
  public ConfigureSwaggerOptions()
  {
  }

  public void Configure(SwaggerGenOptions options)
  {

    var scheme = GetJwtSecurityScheme();
    options.AddSecurityDefinition(scheme.Reference.Id, scheme);
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
                {scheme, new string[0]}
            });
  }

  private OpenApiInfo CreateVersionInfo()
  {
    var info = new OpenApiInfo {
      Title = "CwkSocial",
      Version = "V1"
    };

    return info;
  }

  private OpenApiSecurityScheme GetJwtSecurityScheme()
  {
    return new OpenApiSecurityScheme {
      Name = "JWT Authentication",
      Description = "Provide a JWT Bearer",
      In = ParameterLocation.Header,
      Type = SecuritySchemeType.Http,
      Scheme = "bearer",
      BearerFormat = "JWT",
      Reference = new OpenApiReference {
        Id = JwtBearerDefaults.AuthenticationScheme,
        Type = ReferenceType.SecurityScheme
      }
    };
  }
}
