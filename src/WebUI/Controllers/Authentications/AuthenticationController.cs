namespace TaskTimeManage.Api.Controllers.Authentications;

[Route("api/[controller]")]
[ApiController]
public partial class AuthenticationController : ControllerBase //NOSONAR
{
  private readonly IConfiguration _configuration;
  private readonly IMediator _mediator;

  public AuthenticationController(IMediator mediator, IConfiguration configuration)
  {
    _mediator = mediator;
    _configuration = configuration;
  }
}