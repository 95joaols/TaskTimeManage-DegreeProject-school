using Application.Common.Models.Generated;
using Application.CQRS.WorkItems.Commands;
using Application.CQRS.WorkTimes.Commands;

using Domain.Entities;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using WebUI.Requests;

namespace TaskTimeManage.Api.Controllers.WorkItems;

public partial class WorkItemController
{
	[HttpPut]
	[Authorize]
	public async Task<ActionResult<WorkItemDto>> EditWorkItemAsync([FromBody] EditWorkItemRequest reqest, CancellationToken cancellationToken = default)
	{
		try
		{
			WorkItem workItem = await mediator.Send(new UpdateWorkItemCommand(reqest.PublicId, reqest.Name), cancellationToken);
			if (reqest.WorkTimes.Any())
			{
				_ = await mediator.Send(new UpdateWorkTimesCommand(reqest.WorkTimes), cancellationToken);
			}

			if (workItem != null)
			{
				return Ok(mapper.Map<WorkItemDto>(workItem));
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
}
