﻿using Application.Common.Exceptions;
using Application.Common.Settings;
using Application.CQRS.Authentication.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebUI.Contracts.Authentications.Requests;

namespace TaskTimeManage.Api.Controllers.Authentications;

public partial class AuthenticationController //NOSONAR
{
  [HttpPost("Login")]
  [AllowAnonymous]
  public async Task<ActionResult<string>> LoginAsync([FromBody] UserRequest reqest, CancellationToken cancellationToken)
  {
    if (reqest is null || string.IsNullOrWhiteSpace(reqest.Username) || string.IsNullOrWhiteSpace(reqest.Password))
    {
      return BadRequest();
    }

    try
    {
      IConfigurationSection? appSettingsSection = _configuration.GetSection("ApplicationSecuritySettings");
      ApplicationSecuritySettings? applicationSecuritySettings = appSettingsSection.Get<ApplicationSecuritySettings>();

      string token =
        await _mediator.Send(new LoginQuery(reqest.Username, reqest.Password, applicationSecuritySettings.Secret),
          cancellationToken);
      if (string.IsNullOrWhiteSpace(token))
      {
        return Problem(statusCode: 500);
      }

      return Ok(token);
    }
    catch (LogInWrongException e)
    {
      return Problem(title: e.Message, detail: e.Message, statusCode: 400);
    }
    catch (Exception e)
    {
      return Problem(e.Message, statusCode: 500);
    }
  }
}