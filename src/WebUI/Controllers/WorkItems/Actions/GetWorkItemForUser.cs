using Application.Common.Models.Generated;
using Application.CQRS.WorkItems.Queries;

using Domain.Entities;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TaskTimeManage.Api.Controllers.WorkItems;

public partial class WorkItemController //NOSONAR
{
  [HttpGet("UserId/{UserId}")]
  [Authorize]
  public async Task<ActionResult<IEnumerable<WorkItemDto>>> GetWorkItemForUserAsync(Guid userId, CancellationToken cancellationToken)
  {
    try
    {
      IEnumerable<WorkItem> WorkItems = await mediator.Send(new GetWorkItemTimeByUserPublicIdQuery(userId), cancellationToken);

      if (WorkItems.Any())
      {
        return Ok(mapper.Map<IEnumerable<WorkItemDto>>(WorkItems.OrderByDescending(o => o.Id).ToList()));
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
