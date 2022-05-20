namespace WebUI.Registrars;

public interface IWebApplicationRegistrar : IRegistrar
{
  public void RegisterPipelineComponents(WebApplication app);
}
