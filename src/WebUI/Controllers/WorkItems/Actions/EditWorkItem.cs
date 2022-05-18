using Application.CQRS.WorkItems.Commands;
using Application.CQRS.WorkTimes.Commands;
using Domain.Aggregates.WorkAggregate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebUI.Contracts.WorkItems.Requests;
using WebUI.Contracts.WorkItems.Responds;

namespace TaskTimeManage.Api.Controllers.WorkItems;

public partial class WorkItemController //NOSONAR
{
  [HttpPut]
  [Authorize]
  public async Task<ActionResult<WorkItemRespond>> EditWorkItemAsync([FromBody] EditWorkItemRequest reqest,
    CancellationToken cancellationToken)
  {
    try
    {
      WorkItem workItem =
        await _mediator.Send(new UpdateWorkItemCommand(reqest.PublicId, reqest.Name), cancellationToken);
      if (reqest.WorkTimes.Any())
      {
        _ = await _mediator.Send(
          new UpdateWorkTimesCommand(
            reqest.WorkTimes.Select(x => WorkTime.CreateWorkTime(x.PublicId, x.Time, workItem))),
          cancellationToken);
      }

      if (workItem != null)
      {
        return Ok(_mapper.Map<WorkItemRespond>(workItem));
      }

      return Problem(title: "Error Edit WorkItem", detail: "Did not Edit the WorkItem", statusCode: 500);

    }
    catch (Exception ex)
    {
      return Problem(title: "Error Edit WorkItem", detail: ex.Message, statusCode: 500);
    }
  }
}