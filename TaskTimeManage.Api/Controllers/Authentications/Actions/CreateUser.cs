﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using TaskTimeManage.Api.Requests;
using TaskTimeManage.Core.Commands.Authentication;
using TaskTimeManage.Core.Exceptions;
using TaskTimeManage.Core.Models;

namespace TaskTimeManage.Api.Controllers.Authentications;

public partial class AuthenticationController
{
	[HttpPost("CreateUser")]
	[AllowAnonymous]
	public async Task<ActionResult> CreateUserAsync([FromBody] UserRequest reqest, CancellationToken cancellationToken = default)
	{
		if (reqest is null || string.IsNullOrWhiteSpace(reqest.Username) || string.IsNullOrWhiteSpace(reqest.Password))
		{
			return BadRequest("Name and Password is needed");
		}
		try
		{
			UserModel user = await mediator.Send(new RegistrateUserCommand(reqest.Username, reqest.Password), cancellationToken);
			if (user is not null && user.Id != 0)
			{
				return Created("", true);
			}
			else
			{
				return Problem(statusCode: 500);
			}
		}
		catch (UserAlreadyExistsException e)
		{
			return Problem(title: e.Message, detail: e.Message, statusCode: 400);

		}
		catch (Exception e)
		{

			return Problem(detail: e.Message, statusCode: 500);
		}
	}
}
