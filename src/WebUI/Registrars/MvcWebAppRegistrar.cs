using Microsoft.AspNetCore.HttpOverrides;

namespace WebUI.Registrars;

public class MvcWebAppRegistrar : IWebApplicationRegistrar
{
  public void RegisterPipelineComponents(WebApplication app)
  {
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseForwardedHeaders(new ForwardedHeadersOptions {
      ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
    });

    app.UseHttpsRedirection();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

  }
}
