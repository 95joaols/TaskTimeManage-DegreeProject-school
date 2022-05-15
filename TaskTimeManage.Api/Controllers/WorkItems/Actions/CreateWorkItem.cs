using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using TaskTimeManage.Api.Requests;
using TaskTimeManage.Api.Responses;
using TaskTimeManage.Core.Commands.WorkItems;
using TaskTimeManage.Core.Models;

namespace TaskTimeManage.Api.Controllers.WorkItems;

public partial class WorkItemController
{
	[HttpPost]
	[Authorize]
	public async Task<ActionResult<WorkItemRespons>> CreateWorkItemAsync([FromBody] CreateWorkItemRequest reqest, CancellationToken cancellationToken = default)
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
}
