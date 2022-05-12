using AutoMapper;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using TaskTimeManage.Api.Dtos.Requsts;
using TaskTimeManage.Api.Dtos.Responses;
using TaskTimeManage.MediatR.Commands.WorkItems;
using TaskTimeManage.MediatR.Models;
using TaskTimeManage.MediatR.Queries.WorkItems;

namespace TaskTimeManage.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public partial class WorkItemController : ControllerBase
{
	private readonly IMediator mediator;
	private readonly IConfiguration configuration;
	private readonly IMapper mapper;

	public WorkItemController(IMediator mediator, IConfiguration configuration, IMapper mapper)
	{
		this.mediator = mediator;
		this.configuration = configuration;
		this.mapper = mapper;
	}

	[HttpGet("{PublicId}")]
	[Authorize]
	public async Task<ActionResult<IEnumerable<WorkItemRespons>>> GetWorkItem(Guid PublicId, CancellationToken cancellationToken = default)
	{
		try
		{
			WorkItemModel? workItemModel = await mediator.Send(new GetWorkItemWithWorkTimeByPublicIdQuery(PublicId), cancellationToken);

			if (workItemModel != null)
			{
				return Ok(mapper.Map<IEnumerable<WorkItemWithWorkTime>>(workItemModel));
			}
			else
			{
				return NoContent();
			}
		}
		catch (Exception ex)
		{
			return Problem(title: ex.Message, statusCode: 500);
		}
	}

	[HttpGet("UserId/{UserId}")]
	[Authorize]
	public async Task<ActionResult<IEnumerable<WorkItemRespons>>> GetWorkItemForUser(Guid userId, CancellationToken cancellationToken = default)
	{
		try
		{
			IEnumerable<WorkItemModel> WorkItemModels = await mediator.Send(new GetWorkItemTimeByUserPublicIdQuery(userId), cancellationToken);

			if (WorkItemModels.Any())
			{
				return Ok(mapper.Map<IEnumerable<WorkItemRespons>>(WorkItemModels));
			}
			else
			{
				return NoContent();
			}
		}
		catch (Exception ex)
		{
			return Problem(title: ex.Message, statusCode: 500);
		}
	}

	[HttpPost]
	[Authorize]
	public async Task<ActionResult<WorkItemRespons>> CreateWorkItem([FromBody] CreateWorkItemRequsts reqest, CancellationToken cancellationToken = default)
	{
		try
		{
			WorkItemModel workItemModel = await mediator.Send(new CreateNewWorkItemCommand(reqest.Name, reqest.UserPublicId), cancellationToken);

			if (workItemModel != null)
			{
				return Created("", mapper.Map<WorkItemRespons>(workItemModel));
			}
			else
			{
				return Problem(title: "Error");
			}
		}
		catch (Exception ex)
		{
			return Problem(title: ex.Message, statusCode: 500);
		}
	}

	[HttpPut]
	[Authorize]
	public async Task<ActionResult<WorkItemRespons>> EditWorkItem([FromBody] EditWorkItemRequsts reqest, CancellationToken cancellationToken = default)
	{
		try
		{
			WorkItemModel workItemModel = await mediator.Send(new UpdateWorkItemCommand(reqest.PublicId, reqest.Name), cancellationToken);

			if (workItemModel != null)
			{
				return Ok(mapper.Map<WorkItemRespons>(workItemModel));
			}
			else
			{
				return Problem(title: "Error");
			}
		}
		catch (Exception ex)
		{
			return Problem(title: ex.Message, statusCode: 500);
		}
	}

	[HttpDelete("{publicId:Guid}")]
	[Authorize]
	public async Task<ActionResult<bool>> DeleteWorkItem(Guid publicId, CancellationToken cancellationToken = default)
	{
		try
		{
			if (await mediator.Send(new DeleteWorkItemCommand(publicId), cancellationToken))
			{

				return Ok(true);
			}
			else
			{
				return Problem(title: "");
			}
		}
		catch (Exception ex)
		{
			return Problem(title: ex.Message, statusCode: 500);
		}
	}
}
