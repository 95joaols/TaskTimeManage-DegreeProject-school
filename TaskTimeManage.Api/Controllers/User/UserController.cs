using Microsoft.AspNetCore.Mvc;

using TaskTimeManage.Core.Service;

namespace TaskTimeManage.Api.Controllers.User;

[Route("api/[controller]")]
[ApiController]
public partial class UserController : ControllerBase
{
	private readonly UserService userService;
	private readonly IConfiguration configuration;

	public UserController(UserService userService, IConfiguration configuration)
	{
		this.userService = userService;
		this.configuration = configuration;
	}
}
