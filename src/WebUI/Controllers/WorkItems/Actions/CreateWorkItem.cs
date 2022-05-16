using Application.Common.Models.Generated;
using Application.CQRS.WorkItems.Commands;

using Domain.Entities;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using WebUI.Requests;

namespace TaskTimeManage.Api.Controllers.WorkItems;

public partial class WorkItemController
{
	[HttpPost]
	[Authorize]
	public async Task<ActionResult<WorkItemDto>> CreateWorkItemAsync([FromBody] CreateWorkItemRequest reqest, CancellationToken cancellationToken = default)
	{
		try
		{
			WorkItem workItem = await mediator.Send(new CreateNewWorkItemCommand(reqest.Name, reqest.UserPublicId), cancellationToken);

			if (workItem != null)
			{
				return Created("", mapper.Map<WorkItemDto>(workItem));
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
