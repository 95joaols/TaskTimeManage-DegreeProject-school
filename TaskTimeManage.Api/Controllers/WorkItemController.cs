using AutoMapper;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using TaskTimeManage.Api.Requests;
using TaskTimeManage.Api.Responses;
using TaskTimeManage.Core.Commands.WorkItems;
using TaskTimeManage.Core.Commands.WorkTimes;
using TaskTimeManage.Core.Models;
using TaskTimeManage.Core.Queries.WorkItems;

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
	public async Task<ActionResult<WorkItemRespons>> GetWorkItem(Guid PublicId, CancellationToken cancellationToken = default)
	{
		try
		{
			WorkItemModel? workItemModel = await mediator.Send(new GetWorkItemWithWorkTimeByPublicIdQuery(PublicId), cancellationToken);

			if (workItemModel != null)
			{
				workItemModel.WorkTimes = workItemModel.WorkTimes.OrderBy(o => o.Time).ToList();
				return Ok(mapper.Map<WorkItemWithWorkTime>(workItemModel));
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
				return Ok(mapper.Map<IEnumerable<WorkItemRespons>>(WorkItemModels.OrderByDescending(o => o.Id).ToList()));
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
	public async Task<ActionResult<WorkItemRespons>> CreateWorkItem([FromBody] CreateWorkItemRequest reqest, CancellationToken cancellationToken = default)
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
	public async Task<ActionResult<WorkItemRespons>> EditWorkItem([FromBody] EditWorkItemRequest reqest, CancellationToken cancellationToken = default)
	{
		try
		{
			WorkItemModel workItemModel = await mediator.Send(new UpdateWorkItemCommand(reqest.PublicId, reqest.Name), cancellationToken);
			if (reqest.WorkTimes.Any())
			{
				await mediator.Send(new UpdateWorkTimesCommand(reqest.WorkTimes), cancellationToken);
			}

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
