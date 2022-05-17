using Application.Common.Mappings;

using MediatR;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using System.Reflection;

namespace Application;
public static class DependencyInjection
{
  public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
  {
    _ = services.AddMediatR(Assembly.GetExecutingAssembly());
    _ = services.AddAutoMapper(typeof(MappingProfile));

    return services;
  }
}
