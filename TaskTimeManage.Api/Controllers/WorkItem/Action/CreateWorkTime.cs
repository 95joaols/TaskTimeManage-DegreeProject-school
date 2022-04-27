using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskTimeManage.Domain.Entity;

using TaskTimeManage.Domain.Dto;
using TaskTimeManage.Domain.Dto.Mapping;

namespace TaskTimeManage.Api.Controllers.WorkItem;

public partial class WorkItemController
{
	[HttpPost("CreateWorkTime")]
	[Authorize]
	public async Task<ActionResult<IEnumerable<Domain.Entity.WorkItem>>> CreateWorkTime(WorkTime workTime, Guid publicId, CancellationToken cancellationToken = default)
	{
		Domain.Entity.WorkItem? workItem = await workItemService.GetWorkItemAsync(publicId, cancellationToken);
		if (workItem == null)
		{
			return BadRequest();
		}
		try
		{
			await workTimeService.CreateWorkTimeAsync(workTime, publicId, cancellationToken);
			return Ok();
		}
		catch (Exception ex)
		{
			return Problem(title: ex.Message);
		}
	}
}
