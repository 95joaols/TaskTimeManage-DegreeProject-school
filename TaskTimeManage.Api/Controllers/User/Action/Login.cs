using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using TaskTimeManage.Domain.DTO;
using TaskTimeManage.Domain.Exceptions;

namespace TaskTimeManage.Api.Controllers.User;

public partial class UserController
{
	[HttpPost("Login")]
	[AllowAnonymous]
	public async Task<ActionResult<string>> Login([FromBody] UserDTO LoginUserDTO, CancellationToken cancellationToken = default)
	{
		if (LoginUserDTO is null || string.IsNullOrWhiteSpace(LoginUserDTO.Name) || string.IsNullOrWhiteSpace(LoginUserDTO.Password))
		{
			return BadRequest();
		}
		string token = "";
		try
		{
			token = await userService.LoginAsync(LoginUserDTO.Name, LoginUserDTO.Password, configuration.GetSection("AppSettings:Token").Value, cancellationToken);

		}
		catch (LogInWrongException e)
		{
			return Problem(title: e.Message, detail: e.Message, statusCode: 400);;
		}
		catch (Exception e)
		{

			return Problem(detail: e.Message, statusCode: 500);
		}

		if (!string.IsNullOrWhiteSpace(token))
		{
			return Ok(token);
		}
		else
		{
			return Problem(statusCode: 500);
		}
	}
}
