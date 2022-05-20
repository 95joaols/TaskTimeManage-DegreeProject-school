using Application.Common.Interfaces;

using Infrastructure.Persistence;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace WebUI.Registrars;

public class DbRegistrar : IWebApplicationBuilderRegistrar
{
  public void RegisterServices(WebApplicationBuilder builder)
  {
    var cs = builder.Configuration.GetConnectionString("TaskTimeManagePostgres");
    builder.Services.AddDbContext<ApplicationDbContext>(options => {
      options.UseNpgsql(cs, b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));
    });

    builder.Services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());
    builder.Services.AddScoped<IApplicationDbContextWithTransaction>(provider => provider.GetService<ApplicationDbContext>());

    builder.Services.AddIdentityCore<IdentityUser>(options => {
      options.Password.RequireDigit = false;
      options.Password.RequiredLength = 5;
      options.Password.RequireLowercase = false;
      options.Password.RequireUppercase = false;
      options.Password.RequireNonAlphanumeric = false;
    })
        .AddEntityFrameworkStores<ApplicationDbContext>();

   

  }
}
