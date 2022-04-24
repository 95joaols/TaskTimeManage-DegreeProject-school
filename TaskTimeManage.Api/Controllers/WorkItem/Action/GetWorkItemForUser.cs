using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System;

namespace TaskTimeManage.Api.Controllers.WorkItem;

public partial class WorkItemController
{
	[HttpGet("GetWorkItemForUser/{UserId}")]
	[Authorize]
	public async Task<ActionResult<IEnumerable<Domain.Entity.WorkItem>>> GetWorkItemForUser(Guid userId, CancellationToken cancellationToken = default)
	{
		Domain.Entity.User? user = await userService.GetUserByPublicIdAsync(userId);
		if (user?.Tasks.Any() ?? false)
		{
			return user.Tasks;
		}
		else
		{
			return NoContent();
		}
	}
}
