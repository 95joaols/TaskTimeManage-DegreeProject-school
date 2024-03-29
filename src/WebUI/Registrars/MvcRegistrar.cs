﻿namespace WebUI.Registrars;

public class MvcRegistrar : IWebApplicationBuilderRegistrar
{
  public void RegisterServices(WebApplicationBuilder builder)
  {
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
  }
}