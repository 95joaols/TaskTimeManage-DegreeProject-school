using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using TaskTimeManage.Domain.Entity;

namespace TaskTimeManage.Api.Controllers.WorkItem;

public partial class WorkItemController
{
	[HttpDelete("WorkTime/{publicId:Guid}")]
	[Authorize]
	public async Task<ActionResult<bool>> DeleteWorkTime(Guid publicId, [FromBody] WorkTime workTime, CancellationToken cancellationToken = default)
	{
		try
		{
			return Ok(await workTimeService.DeleteWorkTimeAsync(workTime.PublicId, publicId, cancellationToken));
		}
		catch (Exception ex)
		{
			return Problem(title: ex.Message);
		}
	}
}
