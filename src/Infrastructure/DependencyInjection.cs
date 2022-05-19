using Application.Common.Interfaces;
using Application.Common.Settings;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Infrastructure;

public static class DependencyInjection
{
  public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
  {
    _ = services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(
      configuration.GetConnectionString("TaskTimeManagePostgres"),
      b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

    _ = services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());

    _ = services.AddRouting(x => x.LowercaseUrls = true);

    return services;
  }
}