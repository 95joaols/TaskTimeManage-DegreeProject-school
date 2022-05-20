using Application.CQRS.WorkItems.Commands;
using Domain.Aggregates.WorkAggregate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebUI.Contracts.WorkItems.Requests;
using WebUI.Contracts.WorkItems.Responds;

namespace TaskTimeManage.Api.Controllers.WorkItems;

public partial class WorkItemController //NOSONAR
{
  [HttpPost]
  [Authorize]
  public async Task<ActionResult<WorkItemRespond>> CreateWorkItemAsync([FromBody] CreateWorkItemRequest reqest,
    CancellationToken cancellationToken)
  {
    try
    {
      WorkItem workItem = await _mediator.Send(new CreateNewWorkItemCommand(reqest.Name, reqest.UserPublicId),
        cancellationToken);

      if (workItem != null)
      {
        return Created("", _mapper.Map<WorkItemRespond>(workItem));
      }

      return Problem(title: "Error Create WorkItem", detail: "Did not create WorkItem", statusCode: 500);
    }
    catch (Exception ex)
    {
      return Problem(title: "Error Create WorkItem", detail: ex.Message, statusCode: 500);
    }
  }
}