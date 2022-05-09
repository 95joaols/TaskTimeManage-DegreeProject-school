using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using TaskTimeManage.Domain.Entity;

namespace TaskTimeManage.Api.Controllers.WorkItem;

public partial class WorkItemController
{
	[HttpPut("WorkTime/{publicId:Guid}")]
	[Authorize]
	public async Task<ActionResult<IEnumerable<Domain.Entity.WorkItem>>> EditWorkTime(Guid publicId, [FromBody] WorkTime workTime, CancellationToken cancellationToken = default)
	{
		Domain.Entity.WorkItem? workItem = await workItemService.GetWorkItemAsync(publicId, cancellationToken);
		if (workItem == null)
		{
			return BadRequest();
		}
		try
		{
			WorkTime workTime = await workTimeService.CreateWorkTimeAsync(createWorkTimeDto.WorkTime, createWorkTimeDto.PublicId, cancellationToken);
			return Created("", workTime);
		}
		catch (Exception ex)
		{
			return Problem(title: ex.Message);
		}
	}
}
