namespace WebUI.Extensions;

public static class RegistrarExtensions
{
  public static void RegisterServices(this WebApplicationBuilder builder, Type scanningType)
  {
    IEnumerable<IWebApplicationBuilderRegistrar> registrars =
      GetRegistrars<IWebApplicationBuilderRegistrar>(scanningType);

    foreach (IWebApplicationBuilderRegistrar registrar in registrars)
    {
      registrar.RegisterServices(builder);
    }
  }

  public static void RegisterPipelineComponents(this WebApplication app, Type scanningType)
  {
    IEnumerable<IWebApplicationRegistrar> registrars = GetRegistrars<IWebApplicationRegistrar>(scanningType);
    foreach (IWebApplicationRegistrar registrar in registrars)
    {
      registrar.RegisterPipelineComponents(app);
    }
  }

  private static IEnumerable<T> GetRegistrars<T>(Type scanningType) where T : IRegistrar =>
    scanningType.Assembly.GetTypes()
      .Where(t => t.IsAssignableTo(typeof(T)) && !t.IsAbstract && !t.IsInterface)
      .Select(Activator.CreateInstance)
      .Cast<T>();
}