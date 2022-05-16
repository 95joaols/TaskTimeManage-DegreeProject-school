using AutoMapper;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace TaskTimeManage.Api.Controllers.WorkItems;

[Route("api/[controller]")]
[ApiController]
public partial class WorkItemController : ControllerBase
{
	private readonly IMediator mediator;
	private readonly IMapper mapper;

	public WorkItemController(IMediator mediator, IMapper mapper)
	{
		this.mediator = mediator;
		this.mapper = mapper;
	}
}
