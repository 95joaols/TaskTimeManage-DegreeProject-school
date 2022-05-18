using Application.Common.Exceptions;
using Application.CQRS.Authentication.Commands;
using Ardalis.GuardClauses;
using Domain.Aggregates.UserAggregate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebUI.Contracts.Authentications.Requests;

namespace TaskTimeManage.Api.Controllers.Authentications;

public partial class AuthenticationController //NOSONAR
{
  [HttpPost("CreateUser")]
  [AllowAnonymous]
  public async Task<ActionResult> CreateUserAsync([FromBody] UserRegistrantsRequest reqest,
    CancellationToken cancellationToken)
  {
    try
    {
      _ = Guard.Against.Null(reqest);
      _ = Guard.Against.NullOrWhiteSpace(reqest.Username);
      _ = Guard.Against.NullOrWhiteSpace(reqest.Password);
      _ = Guard.Against.NullOrWhiteSpace(reqest.RepeatPassword);

      if (reqest.Password != reqest.RepeatPassword)
      {
        throw new PasswordNotSameException();
      }

      UserProfile user =
        await _mediator.Send(new RegistrateUserCommand(reqest.Username, reqest.Password), cancellationToken);
      if (user is not null && user.Id != 0)
      {
        return Created("", true);
      }

      return Problem(statusCode: 500);
    }
    catch (Exception ex)
    {
      if (ex is PasswordNotSameException or UserAlreadyExistsException)
      {
        return Problem(title: "Error Create User", detail: ex.Message, statusCode: 400);
      }

      return Problem(title: "Error Create User", detail: ex.Message, statusCode: 500);
    }
  }
}