using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using TaskTimeManage.Domain.DTO;
using TaskTimeManage.Domain.Exceptions;

namespace TaskTimeManage.Api.Controllers.User;

public partial class UserController
{
	[HttpPost("CreateUser")]
	[AllowAnonymous]
	public async Task<ActionResult> CreateUserAsync([FromBody] UserDTO createUserDTO, CancellationToken cancellationToken = default)
	{
		if (createUserDTO is null || string.IsNullOrWhiteSpace(createUserDTO.Name) || string.IsNullOrWhiteSpace(createUserDTO.Password))
		{
			return BadRequest("Name and Password is needed");
		}
		Domain.Entity.User user;
		try
		{

			user = await userService.CreateUserAsync(createUserDTO.Name, createUserDTO.Password, cancellationToken);
		}
		catch (UserAlreadyExistsException e)
		{
			return Problem(title: e.Message, detail: e.Message, statusCode: 400);
			
		}
		catch (Exception e)
		{

			return Problem(detail: e.Message, statusCode: 500);
		}
		if (user is not null && user.Id != 0)
		{
			return Created("", true);
		}
		else
		{
			return Problem(statusCode: 500);
		}
	}
}
