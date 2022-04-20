using Microsoft.AspNetCore.Mvc;

using TaskTimeManage.Domain.DTO;

namespace TaskTimeManage.Api.Controllers.User;

public partial class UserController
{
	[HttpPost]
	public async Task<ActionResult> CreateUserAsync([FromBody] UserDTO createUserDTO, CancellationToken cancellationToken = default)
	{
		if (createUserDTO is null || string.IsNullOrWhiteSpace(createUserDTO.Name) || string.IsNullOrWhiteSpace(createUserDTO.Password))
		{
			return BadRequest();
		}
		Domain.Entity.User user = await userService.CreateUserAsync(createUserDTO.Name, createUserDTO.Password, cancellationToken);

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
