namespace WebUI.Registrars;

public interface IWebApplicationBuilderRegistrar : IRegistrar
{
  void RegisterServices(WebApplicationBuilder builder);
}