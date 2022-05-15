using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using TaskTimeManage.Api.Requests;
using TaskTimeManage.Core.Exceptions;
using TaskTimeManage.Core.Queries.Authentication;

namespace TaskTimeManage.Api.Controllers.Authentications;

public partial class AuthenticationController
{
	[HttpPost("Login")]
	[AllowAnonymous]
	public async Task<ActionResult<string>> LoginAsync([FromBody] UserRequest reqest, CancellationToken cancellationToken = default)
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
}
