using Microsoft.AspNetCore.Mvc;

using TaskTimeManage.Domain.DTO;
using TaskTimeManage.Domain.Entity;

namespace TaskTimeManage.Api.Controllers.UserC;

public partial class UserController
{
	[HttpPost]
	public async Task<ActionResult> CreateUserAsync([FromBody] CreateUserDTO createUserDTO, CancellationToken cancellationToken = default)
	{
		if (createUserDTO is null || string.IsNullOrWhiteSpace(createUserDTO.Name) || string.IsNullOrWhiteSpace(createUserDTO.Password))
		{
			return BadRequest();
		}
		User user = await userService.CreateUserAsync(createUserDTO.Name, createUserDTO.Password);

		if (user is not null && user.Id != 0)
		{
			return Created("", null);
		}
		else
		{
			return Problem(statusCode: 500);
		}
	}
}
