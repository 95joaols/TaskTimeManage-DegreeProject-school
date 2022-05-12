using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using TaskTimeManage.Api.Requests;
using TaskTimeManage.Core.Commands.Authentication;
using TaskTimeManage.Core.Exceptions;
using TaskTimeManage.Core.Models;
using TaskTimeManage.Core.Queries.Authentication;

namespace TaskTimeManage.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public partial class AuthenticationController : ControllerBase
{
	private readonly IMediator mediator;
	private readonly IConfiguration configuration;

	public AuthenticationController(IMediator mediator, IConfiguration configuration)
	{
		this.mediator = mediator;
		this.configuration = configuration;
	}

	[HttpPost("Login")]
	[AllowAnonymous]
	public async Task<ActionResult<string>> Login([FromBody] UserRequest reqest, CancellationToken cancellationToken = default)
	{
		if (reqest is null || string.IsNullOrWhiteSpace(reqest.Username) || string.IsNullOrWhiteSpace(reqest.Password))
		{
			return BadRequest();
		}
		try
		{
			string token = await mediator.Send(new LoginQuery(reqest.Username, reqest.Password, configuration.GetSection("AppSettings:Token").Value), cancellationToken);
			if (string.IsNullOrWhiteSpace(token))
			{
				return Problem(statusCode: 500);
			}
			else
			{
				return Ok(token);
			}
		}
		catch (LogInWrongException e)
		{
			return Problem(title: e.Message, detail: e.Message, statusCode: 400);

		}
		catch (Exception e)
		{

			return Problem(detail: e.Message, statusCode: 500);
		}
	}
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
