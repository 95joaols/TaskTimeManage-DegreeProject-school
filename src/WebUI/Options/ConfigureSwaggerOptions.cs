namespace WebUI.Options;

public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
{
  public void Configure(SwaggerGenOptions options)
  {
    var scheme = GetJwtSecurityScheme();
    options.AddSecurityDefinition(scheme.Reference.Id, scheme);
    options.AddSecurityRequirement(new OpenApiSecurityRequirement {
        { scheme, new string[0] }
      }
    );
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
        Id = JwtBearerDefaults.AuthenticationScheme,
        Type = ReferenceType.SecurityScheme
      }
    };
}