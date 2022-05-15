using MediatR;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

using Swashbuckle.AspNetCore.Filters;

using System.Text;

using TaskTimeManage.Core;
using TaskTimeManage.Core.DataAccess;

WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => {
	options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme {
		Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
		In = ParameterLocation.Header,
		Name = "Authorization",
		Type = SecuritySchemeType.ApiKey
	});

	options.OperationFilter<SecurityRequirementsOperationFilter>();
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
		.AddJwtBearer(options => {
			options.TokenValidationParameters = new TokenValidationParameters {
				ValidateIssuerSigningKey = true,
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
							.GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
				ValidateIssuer = false,
				ValidateAudience = false
			};
		});

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddMediatR(typeof(MediatREntrypoint).Assembly);

string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<TTMDataAccess>(option => option.UseNpgsql(connectionString, x => x.MigrationsAssembly("TaskTimeManage.Migrations")));


WebApplication? app = builder.Build();

// migrate any database changes on startup (includes initial db creation)
using (IServiceScope? scope = app.Services.CreateScope())
{
	TTMDataAccess? dataContext = scope.ServiceProvider.GetRequiredService<TTMDataAccess>();
	dataContext.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseCors(o => o.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseForwardedHeaders(new ForwardedHeadersOptions {
	ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
