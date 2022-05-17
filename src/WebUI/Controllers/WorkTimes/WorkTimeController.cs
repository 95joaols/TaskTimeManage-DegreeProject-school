using AutoMapper;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace TaskTimeManage.Api.Controllers.WorkTimes;

[Route("api/[controller]")]
[ApiController]
public partial class WorkTimeController : ControllerBase //NOSONAR
{
  private readonly IMediator mediator;
  private readonly IMapper mapper;

  public WorkTimeController(IMediator mediator, IMapper mapper)
  {
    this.mediator = mediator;
    this.mapper = mapper;
  }
}
