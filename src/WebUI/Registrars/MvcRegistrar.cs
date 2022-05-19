namespace WebUI.Registrars;

public class MvcRegistrar : IWebApplicationBuilderRegistrar
{
  public void RegisterServices(WebApplicationBuilder builder)
  {
    builder.Services.AddEndpointsApiExplorer();
  }
}
