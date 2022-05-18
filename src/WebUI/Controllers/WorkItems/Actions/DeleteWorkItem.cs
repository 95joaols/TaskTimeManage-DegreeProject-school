using Application.CQRS.WorkItems.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TaskTimeManage.Api.Controllers.WorkItems;

public partial class WorkItemController //NOSONAR
{
  [HttpDelete("{publicId:Guid}")]
  [Authorize]
  public async Task<ActionResult<bool>> DeleteWorkItem(Guid publicId, CancellationToken cancellationToken)
  {
    try
    {
      if (await _mediator.Send(new DeleteWorkItemCommand(publicId), cancellationToken))
      {
        return Ok(true);
      }

      return Problem(title: "Error Delete WorkItem", detail: "Did not Delete WorkItem", statusCode: 500);

    }
    catch (Exception ex)
    {
      return Problem(title: "Error Delete WorkItem", detail: ex.Message, statusCode: 500);
    }
  }
}