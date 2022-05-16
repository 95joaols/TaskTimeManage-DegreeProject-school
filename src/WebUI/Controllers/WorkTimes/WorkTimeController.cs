using AutoMapper;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace TaskTimeManage.Api.Controllers.WorkTimes;

[Route("api/[controller]")]
[ApiController]
public partial class WorkTimeController : ControllerBase
{
	private readonly IMediator mediator;
	private readonly IConfiguration configuration;
	private readonly IMapper mapper;

	public WorkTimeController(IMediator mediator, IConfiguration configuration, IMapper mapper)
	{
		this.mediator = mediator;
		this.configuration = configuration;
		this.mapper = mapper;
	}
}
