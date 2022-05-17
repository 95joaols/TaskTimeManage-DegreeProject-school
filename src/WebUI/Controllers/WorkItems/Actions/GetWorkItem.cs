﻿using Application.CQRS.WorkItems.Queries;
using Domain.Aggregates.WorkAggregate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebUI.Contracts.WorkItems.Responds;

namespace TaskTimeManage.Api.Controllers.WorkItems;

public partial class WorkItemController //NOSONAR
{
  [HttpGet("{publicId}")]
  [Authorize]
  public async Task<ActionResult<WorkItemRespond>> GetWorkItemAsync(Guid publicId, CancellationToken cancellationToken)
  {
    try
    {
      WorkItem? workItem =
        await _mediator.Send(new GetWorkItemWithWorkTimeByPublicIdQuery(publicId), cancellationToken);

      if (workItem != null)
      {
        WorkItemRespond retunValue = _mapper.Map<WorkItemRespond>(workItem);
        retunValue.WorkTimes = retunValue.WorkTimes.OrderBy(o => o.Time).ToList();
        return Ok(retunValue);
      }

      return NoContent();
    }
    catch (Exception ex)
    {
      return Problem(title: ex.Message, statusCode: 500);
    }
  }
}