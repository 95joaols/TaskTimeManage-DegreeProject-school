using Microsoft.AspNetCore.Mvc;

using TaskTimeManage.Core.Service;

namespace TaskTimeManage.Api.Controllers.UserC;

[Route("api/[controller]")]
[ApiController]
public partial class UserController : ControllerBase
{
	readonly UserService userService;

	public UserController(UserService userService)
	{
		this.userService = userService;
	}
}
