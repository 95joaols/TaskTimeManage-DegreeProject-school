using AutoMapper;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using TaskTimeManage.Api.Dtos.Requsts;
using TaskTimeManage.Api.Dtos.Responses;
using TaskTimeManage.MediatR.Commands.WorkItems;
using TaskTimeManage.MediatR.Commands.WorkTimes;
using TaskTimeManage.MediatR.Models;
using TaskTimeManage.MediatR.Queries.WorkItems;

namespace TaskTimeManage.Api.Controllers;

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
	[HttpPost]
	[Authorize]
	public async Task<ActionResult<WorkTimeRespons>> CreateWorkTime([FromBody] CreateWorkTimeRequest request, CancellationToken cancellationToken = default)
	{
		try
		{
			WorkTimeModel workItemModel = await mediator.Send(new CreateWorkTimeCommand(request.Time, request.WorkItemPublicId), cancellationToken);
			if (workItemModel == null)
			{
				return BadRequest();
			}

			return Created("", mapper.Map<WorkTimeRespons>(workItemModel));
		}
		catch (Exception ex)
		{
			return Problem(title: ex.Message, statusCode: 500);
		}
	}

	[HttpDelete("{publicId:Guid}")]
	[Authorize]
	public async Task<ActionResult<bool>> DeleteWorkTime(Guid publicId, CancellationToken cancellationToken = default)
	{
		try
		{
			if (await mediator.Send(new DeleteWorkTimeByPublicIdCommand(publicId), cancellationToken))
			{

				return Ok(true);
			}
			return Problem(title: "Error");

		}
		catch (Exception ex)
		{
			return Problem(title: ex.Message, statusCode: 500);
		}
	}
}
