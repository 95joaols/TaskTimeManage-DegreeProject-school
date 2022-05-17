using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TaskTimeManage.Api.Controllers.WorkTimes;

[Route("api/[controller]")]
[ApiController]
public partial class WorkTimeController : ControllerBase //NOSONAR
{
  private readonly IMapper _mapper;
  private readonly IMediator _mediator;

  public WorkTimeController(IMediator mediator, IMapper mapper)
  {
    _mediator = mediator;
    _mapper = mapper;
  }
}