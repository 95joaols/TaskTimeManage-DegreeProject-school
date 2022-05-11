﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using TaskTimeManage.Domain.Dto;
using TaskTimeManage.Domain.Dto.Mapping;

namespace TaskTimeManage.Api.Controllers.WorkItem;

public partial class WorkItemController
{
	[HttpGet("WorkItemById/{publicId}")]
	[Authorize]
	public async Task<ActionResult<IEnumerable<WorkItemDto>>> GetWorkItemById(Guid publicId, CancellationToken cancellationToken = default)
	{
		Domain.Entity.WorkItem? workItem = await workItemService.GetWorkItemAsync(publicId, cancellationToken);
		if (workItem != null)
		{
			return Ok(DtoMapping.WorkItemDtoMap(workItem, workItem.User.PublicId));
		}
		else
		{
			return NoContent();
		}
	}


}
