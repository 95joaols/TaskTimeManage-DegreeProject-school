using Application.Common.Interfaces;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace WebUI.Registrars;

public class DbRegistrar : IWebApplicationBuilderRegistrar
{
  public void RegisterServices(WebApplicationBuilder builder)
  {
    string? cs = builder.Configuration.GetConnectionString("TaskTimeManagePostgres");
    builder.Services.AddDbContext<ApplicationDbContext>(options => {
      options.UseNpgsql(cs, b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));
    });

    builder.Services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());
    builder.Services.AddScoped<IApplicationDbContextWithTransaction>(provider =>
      provider.GetService<ApplicationDbContext>());

    builder.Services.AddIdentityCore<IdentityUser>(options => {
        options.Password.RequireDigit = true;
        options.Password.RequiredLength = 8;
        options.Password.RequireLowercase = true;
        options.Password.RequireUppercase = true;
        options.Password.RequireNonAlphanumeric = true;
      })
      .AddEntityFrameworkStores<ApplicationDbContext>();
  }
}