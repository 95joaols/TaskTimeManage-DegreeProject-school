﻿using Application.Common.Security;

namespace WebUI.Registrars;

public class ApplicationLayerRegistrar : IWebApplicationBuilderRegistrar
{
  public void RegisterServices(WebApplicationBuilder builder)
  {
    builder.Services.AddScoped<IdentityService>();
  }
}
