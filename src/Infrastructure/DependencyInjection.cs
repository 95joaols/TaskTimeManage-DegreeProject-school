using Application.Common.Interfaces;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Application.Common.Settings;
using Infrastructure.Persistence;

namespace Infrastructure;
public static class DependencyInjection
{
	public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddDbContext<ApplicationDbContext>(options => {
			services.AddDbContext<ApplicationDbContext>(options => {
				options.UseNpgsql(configuration.GetConnectionString("FlowPostgres"),
								b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName))
						.EnableSensitiveDataLogging();
			});
			services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());

		});

		var appSettingsSection = configuration.GetSection("ApplicationSecuritySettings");
		var applicationSecuritySettings = appSettingsSection.Get<ApplicationSecuritySettings>();
		var key = Encoding.ASCII.GetBytes(applicationSecuritySettings.Secret);
		services.AddAuthentication(x => {
			x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
			x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
		})
		.AddJwtBearer(x => {
			x.RequireHttpsMetadata = false;
			x.SaveToken = true;
			x.TokenValidationParameters = new TokenValidationParameters {
				ValidateIssuerSigningKey = true,
				IssuerSigningKey = new SymmetricSecurityKey(key),
				ValidateIssuer = false,
				ValidateAudience = false
			};
		});
		services.AddRouting(x => x.LowercaseUrls = true);

		return services;
	}
}
