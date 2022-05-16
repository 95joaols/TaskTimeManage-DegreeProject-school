using Application.Common.Exceptions;
using Application.CQRS.Authentication.Commands;

using Ardalis.GuardClauses;

using Domain.Entities;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using WebUI.Requests;

namespace TaskTimeManage.Api.Controllers.Authentications;

public partial class AuthenticationController
{
	[HttpPost("CreateUser")]
	[AllowAnonymous]
	public async Task<ActionResult> CreateUserAsync([FromBody] UserRegistrantsRequest reqest, CancellationToken cancellationToken = default)
	{
		try
		{
			Guard.Against.Null(reqest);
			Guard.Against.NullOrWhiteSpace(reqest.Username);
			Guard.Against.NullOrWhiteSpace(reqest.Password);
			Guard.Against.NullOrWhiteSpace(reqest.RepeatPassword);

			if (reqest.Password != reqest.RepeatPassword)
				throw new PasswordNotSameException();

			User user = await mediator.Send(new RegistrateUserCommand(reqest.Username, reqest.Password), cancellationToken);
			if (user is not null && user.Id != 0)
			{
				return Created("", true);
			}
			else
			{
				return Problem(statusCode: 500);
			}
		}
		catch (Exception ex)
		{
			if (ex is PasswordNotSameException || ex is UserAlreadyExistsException)
			{
				return Problem(title: ex.Message, detail: ex.Message, statusCode: 400);
			}
			return Problem(detail: ex.Message, statusCode: 500);
		}
	}
}
