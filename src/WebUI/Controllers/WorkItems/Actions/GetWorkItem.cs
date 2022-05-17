using Application.Common.Models.Generated;
using Application.CQRS.WorkItems.Queries;

using Domain.Entities;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TaskTimeManage.Api.Controllers.WorkItems;

public partial class WorkItemController //NOSONAR
{
  [HttpGet("{PublicId}")]
  [Authorize]
  public async Task<ActionResult<WorkItemDto>> GetWorkItemAsync(Guid PublicId, CancellationToken cancellationToken)
  {
    try
    {
      WorkItem? workItem = await mediator.Send(new GetWorkItemWithWorkTimeByPublicIdQuery(PublicId), cancellationToken);

      if (workItem != null)
      {
        workItem.WorkTimes = workItem.WorkTimes.OrderBy(o => o.Time).ToList();
        return Ok(mapper.Map<WorkItemDto>(workItem));
      }
      else
      {
        return NoContent();
      }
    }
    catch (Exception ex)
    {
      return Problem(title: ex.Message, statusCode: 500);
    }

  }
}
