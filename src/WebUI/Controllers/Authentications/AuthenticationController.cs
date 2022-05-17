using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace TaskTimeManage.Api.Controllers.Authentications;

[Route("api/[controller]")]
[ApiController]
public partial class AuthenticationController : ControllerBase //NOSONAR
{
  private readonly IMediator mediator;
  private readonly IConfiguration configuration;

  public AuthenticationController(IMediator mediator, IConfiguration configuration)
  {
    this.mediator = mediator;
    this.configuration = configuration;
  }
}
