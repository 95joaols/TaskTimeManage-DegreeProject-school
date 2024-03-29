﻿namespace TaskTimeManage.Api.Controllers.Authentications;

public partial class AuthenticationController //NOSONAR
{
  [HttpPost("CreateUser")]
  [AllowAnonymous]
  public async Task<ActionResult> CreateUserAsync([FromBody] UserRegistrantsRequest reqest,
    CancellationToken cancellationToken)
  {
    try
    {
      Guard.Against.Null(reqest);
      Guard.Against.NullOrWhiteSpace(reqest.Username);
      Guard.Against.NullOrWhiteSpace(reqest.Password);
      Guard.Against.NullOrWhiteSpace(reqest.RepeatPassword);

      if (reqest.Password != reqest.RepeatPassword)
      {
        throw new PasswordNotSameException();
      }

      var user =
        await _mediator.Send(new RegistrateUserCommand(reqest.Username, reqest.Password), cancellationToken);
      if (user is not null && user.Id != 0)
      {
        return Created("", true);
      }

      return Problem(statusCode: 500);
    }
    catch (Exception ex)
    {
      return Problem("Error Create User",
        ex.Message,
        ex is PasswordNotSameException or UserAlreadyExistsException ? 400 : 500
      );
    }
  }
}