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
		_ = services.AddDbContext<ApplicationDbContext>(options => {
			_ = services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("FlowPostgres"),
								b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName))
						.EnableSensitiveDataLogging());
			_ = services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());

		});

		IConfigurationSection? appSettingsSection = configuration.GetSection("ApplicationSecuritySettings");
		ApplicationSecuritySettings? applicationSecuritySettings = appSettingsSection.Get<ApplicationSecuritySettings>();
		byte[]? key = Encoding.ASCII.GetBytes(applicationSecuritySettings.Secret);
		_ = services.AddAuthentication(x => {
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
		_ = services.AddRouting(x => x.LowercaseUrls = true);

		return services;
	}
}
