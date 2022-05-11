using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using TaskTimeManage.Domain.Dto;
using TaskTimeManage.Domain.Entity;

namespace TaskTimeManage.Api.Controllers.WorkItem;

public partial class WorkItemController
{
	[HttpPut]
	[Authorize]
	public async Task<ActionResult<Domain.Entity.WorkItem>> EditWorkItem([FromBody] WorkItemDto workItem, CancellationToken cancellationToken = default)
	{
		try
		{
			return Ok(await workItemService.UpdateAsync(workItem, cancellationToken));
		}
		catch (Exception ex)
		{
			return Problem(title: ex.Message);
		}
	}
}
