using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using TaskTimeManage.Domain.Dto;
using TaskTimeManage.Domain.Exceptions.Mapping;

namespace TaskTimeManage.Api.Controllers.WorkItem;

public partial class WorkItemController
{
	[HttpGet("GetWorkItemForUser/{UserId}")]
	[Authorize]
	public async Task<ActionResult<IEnumerable<WorkItemDto>>> GetWorkItemForUser(Guid userId, CancellationToken cancellationToken = default)
	{
		Domain.Entity.User? user = await userService.GetUserByPublicIdAsync(userId);
		if (user?.Tasks.Any() ?? false)
		{
			return Ok(DtoMapping.ListWorkItemDtoMap(user.Tasks, user.PublicId));
		}
		else
		{
			return NoContent();
		}
	}


}
