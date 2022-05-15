using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using TaskTimeManage.Api.Requests;
using TaskTimeManage.Api.Responses;
using TaskTimeManage.Core.Commands.WorkItems;
using TaskTimeManage.Core.Commands.WorkTimes;
using TaskTimeManage.Core.Models;

namespace TaskTimeManage.Api.Controllers.WorkItems;

public partial class WorkItemController
{
	[HttpPut]
	[Authorize]
	public async Task<ActionResult<WorkItemRespons>> EditWorkItemAsync([FromBody] EditWorkItemRequest reqest, CancellationToken cancellationToken = default)
	{
		try
		{
			WorkItemModel workItemModel = await mediator.Send(new UpdateWorkItemCommand(reqest.PublicId, reqest.Name), cancellationToken);
			if (reqest.WorkTimes.Any())
			{
				_ = await mediator.Send(new UpdateWorkTimesCommand(reqest.WorkTimes), cancellationToken);
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
}
