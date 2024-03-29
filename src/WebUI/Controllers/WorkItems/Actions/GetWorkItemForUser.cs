﻿namespace TaskTimeManage.Api.Controllers.WorkItems;

public partial class WorkItemController //NOSONAR
{
  [HttpGet("UserId/{UserId}")]
  [Authorize]
  public async Task<ActionResult<IEnumerable<WorkItemRespond>>> GetWorkItemForUserAsync(Guid userId,
    CancellationToken cancellationToken)
  {
    try
    {
      IEnumerable<WorkItem> workItems =
        await _mediator.Send(new GetWorkItemTimeByUserPublicIdQuery(userId), cancellationToken);

      if (workItems.Any())
      {
        return Ok(_mapper.Map<IEnumerable<WorkItemRespond>>(workItems.OrderByDescending(o => o.Id).ToList()));
      }

      return NoContent();
    }
    catch (Exception ex)
    {
      return Problem(title: "Error Geting WorkItem", detail: ex.Message, statusCode: 500);
    }
  }
}