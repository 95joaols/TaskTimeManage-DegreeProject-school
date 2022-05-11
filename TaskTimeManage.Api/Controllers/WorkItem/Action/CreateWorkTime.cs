using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using TaskTimeManage.Domain.DTO;
using TaskTimeManage.Domain.Entity;

namespace TaskTimeManage.Api.Controllers.WorkItem;

public partial class WorkItemController
{
	[HttpPost("WorkTime")]
	[Authorize]
	public async Task<ActionResult<IEnumerable<Domain.Entity.WorkItem>>> CreateWorkTime([FromBody] CreateWorkTimeDto createWorkTimeDto, CancellationToken cancellationToken = default)
	{
		Domain.Entity.WorkItem? workItem = await workItemService.GetWorkItemAsync(createWorkTimeDto.PublicId, cancellationToken);
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
