using TaskTimeManage.Core.Service;

namespace TaskTimeManage.Api.Middleware;

public static class CoreService
{
	public static IServiceCollection AddCoreService(this IServiceCollection services) => services
			.AddScoped<UserService>()
			.AddScoped<WorkItemService>()
			.AddScoped<WorkTimeService>();
}
