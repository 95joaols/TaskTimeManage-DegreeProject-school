using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using TaskTimeManage.Core.Models;
using TaskTimeManage.Core.Queries.WorkItems;

using WebUI.Responses;

namespace TaskTimeManage.Api.Controllers.WorkItems;

public partial class WorkItemController
{
	[HttpGet("UserId/{UserId}")]
	[Authorize]
	public async Task<ActionResult<IEnumerable<WorkItemRespons>>> GetWorkItemForUserAsync(Guid userId, CancellationToken cancellationToken = default)
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
}
