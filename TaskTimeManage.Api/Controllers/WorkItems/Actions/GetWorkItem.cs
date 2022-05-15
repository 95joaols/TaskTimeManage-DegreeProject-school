using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using TaskTimeManage.Api.Responses;
using TaskTimeManage.Core.Models;
using TaskTimeManage.Core.Queries.WorkItems;

namespace TaskTimeManage.Api.Controllers.WorkItems;

public partial class WorkItemController
{
	[HttpGet("{PublicId}")]
	[Authorize]
	public async Task<ActionResult<WorkItemRespons>> GetWorkItemAsync(Guid PublicId, CancellationToken cancellationToken = default)
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
}
