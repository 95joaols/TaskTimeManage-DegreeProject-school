using Application.CQRS.WorkItems.Queries;

using Domain.Aggregates.WorkAggregate;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using WebUI.Contracts.WorkItems.Responds;

namespace TaskTimeManage.Api.Controllers.WorkItems;

public partial class WorkItemController //NOSONAR
{
  [HttpGet("UserId/{UserId}")]
  [Authorize]
  public async Task<ActionResult<IEnumerable<WorkItemRespond>>> GetWorkItemForUserAsync(Guid userId, CancellationToken cancellationToken)
  {
    try
    {
      IEnumerable<WorkItem> WorkItems = await mediator.Send(new GetWorkItemTimeByUserPublicIdQuery(userId), cancellationToken);

      if (WorkItems.Any())
      {
        return Ok(mapper.Map<IEnumerable<WorkItemRespond>>(WorkItems.OrderByDescending(o => o.Id).ToList()));
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
