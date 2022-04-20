using Microsoft.AspNetCore.Mvc;

using TaskTimeManage.Core.Service;

namespace TaskTimeManage.Api.Controllers.User;

[Route("api/[controller]")]
[ApiController]
public partial class UserController : ControllerBase
{
	private readonly UserService userService;

	public UserController(UserService userService) => this.userService = userService;
}
