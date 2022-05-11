using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using TaskTimeManage.Domain.Entity;

namespace TaskTimeManage.Api.Controllers.WorkItem;

public partial class WorkItemController
{
	[HttpDelete("{publicId:Guid}")]
	[Authorize]
	public async Task<ActionResult<bool>> DeleteWorkItem(Guid publicId, CancellationToken cancellationToken = default)
	{
		try
		{
			if (await workItemService.DeleteWorkItemAsync(publicId, cancellationToken))
			{

				return Ok(true);
			}
			else
			{
				return Problem(title: "");
			}
		}
		catch (Exception ex)
		{
			return Problem(title: ex.Message);
		}
	}
}
