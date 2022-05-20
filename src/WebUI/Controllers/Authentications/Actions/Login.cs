namespace TaskTimeManage.Api.Controllers.Authentications;

public partial class AuthenticationController //NOSONAR
{
  [HttpPost("Login")]
  [AllowAnonymous]
  public async Task<ActionResult<string>>
    LoginAsync([FromBody] UserRequest reqest, CancellationToken cancellationToken)
  {
    if (reqest is null || string.IsNullOrWhiteSpace(reqest.Username) || string.IsNullOrWhiteSpace(reqest.Password))
    {
      return BadRequest();
    }

    try
    {
      _configuration.GetSection(nameof(JwtSettings)).Get<JwtSettings>();

      string token =
        await _mediator.Send(
          new LoginQuery(reqest.Username, reqest.Password),
          cancellationToken
        );

      return string.IsNullOrWhiteSpace(token) ? Problem(statusCode: 500) : Ok(token);
    }
    catch (LogInWrongException e)
    {
      return Problem(title: "LogIn Error", detail: e.Message, statusCode: 400);
    }
    catch (Exception e)
    {
      return Problem(title: "LogIn Error", detail: e.Message, statusCode: 500);
    }
  }
}