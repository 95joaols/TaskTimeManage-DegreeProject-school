using Application;
using MediatR;

namespace WebUI.Registrars;

public class MediatRRegistrar : IWebApplicationBuilderRegistrar
{
  public void RegisterServices(WebApplicationBuilder builder) => builder.Services.AddMediatR(typeof(MediateRProject));
}