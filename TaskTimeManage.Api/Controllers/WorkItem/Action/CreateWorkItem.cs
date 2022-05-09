using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using TaskTimeManage.Domain.Dto;
using TaskTimeManage.Domain.Dto.Mapping;

namespace TaskTimeManage.Api.Controllers.WorkItem;

public partial class WorkItemController
{
	[HttpPost]
	[Authorize]
	public async Task<ActionResult<IEnumerable<Domain.Entity.WorkItem>>> CreateWorkItem(WorkItemDto createWorkItem, CancellationToken cancellationToken = default)
	{
		Domain.Entity.User? user = await userService.GetUserByPublicIdAsync(createWorkItem.UserId);
		if (user == null)
		{
			return BadRequest("No user found");
		}
		try
		{
			Domain.Entity.WorkItem workItem = await workItemService.CreateTaskAsync(createWorkItem.Name, user, cancellationToken);
			if (workItem == null)
			{
				return Problem(title: "Creating Error", statusCode: 500);
			}
			return Created("", DtoMapping.WorkItemDtoMap(workItem, user.PublicId));
		}
		catch (Exception e)
		{
			return Problem(title: "Creating Error", detail: e.Message, statusCode: 500);
			;
		}

	}
}
