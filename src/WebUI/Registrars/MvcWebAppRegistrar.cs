namespace WebUI.Registrars;

public class MvcWebAppRegistrar : IWebApplicationRegistrar
{
  public void RegisterPipelineComponents(WebApplication app)
  {
    // migrate any database changes on startup (includes initial db creation)
    app.Services.GetRequiredService<ApplicationDbContext>().Database.Migrate();


    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseForwardedHeaders(new ForwardedHeadersOptions {
        ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
      }
    );

    app.UseHttpsRedirection();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();
  }
}