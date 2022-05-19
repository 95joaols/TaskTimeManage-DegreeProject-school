using Application.Common.Service;

namespace WebUI.Registrars;

public class ApplicationLayerRegistrar : IWebApplicationBuilderRegistrar
{
  public void RegisterServices(WebApplicationBuilder builder)
  {
    builder.Services.AddScoped<IdentityService>();
  }
}
