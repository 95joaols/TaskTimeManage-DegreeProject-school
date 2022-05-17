using Application.CQRS.WorkItems.Queries;

using Domain.Aggregates.WorkAggregate;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using WebUI.Contracts.WorkItems.Responds;

namespace TaskTimeManage.Api.Controllers.WorkItems;

public partial class WorkItemController //NOSONAR
{
  [HttpGet("{PublicId}")]
  [Authorize]
  public async Task<ActionResult<WorkItemRespond>> GetWorkItemAsync(Guid PublicId, CancellationToken cancellationToken)
  {
    try
    {
      WorkItem? workItem = await mediator.Send(new GetWorkItemWithWorkTimeByPublicIdQuery(PublicId), cancellationToken);

      if (workItem != null)
      {
        WorkItemRespond retunValue = mapper.Map<WorkItemRespond>(workItem);
        retunValue.workTimes = retunValue.workTimes.OrderBy(o => o.Time).ToList();
        return Ok(retunValue);
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
