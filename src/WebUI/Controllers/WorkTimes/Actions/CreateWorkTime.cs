using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using TaskTimeManage.Core.Commands.WorkTimes;
using TaskTimeManage.Core.Models;

using WebUI.Requests;
using WebUI.Responses;

namespace TaskTimeManage.Api.Controllers.WorkTimes;

public partial class WorkTimeController
{

	[HttpPost]
	[Authorize]
	public async Task<ActionResult<WorkTimeRespons>> CreateWorkTimeAsync([FromBody] CreateWorkTimeRequest request, CancellationToken cancellationToken = default)
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
}
