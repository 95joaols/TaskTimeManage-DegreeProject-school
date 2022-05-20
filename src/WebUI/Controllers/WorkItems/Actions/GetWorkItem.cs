namespace TaskTimeManage.Api.Controllers.WorkItems;

public partial class WorkItemController //NOSONAR
{
  [HttpGet("{publicId}")]
  [Authorize]
  public async Task<ActionResult<WorkItemRespond>> GetWorkItemAsync(Guid publicId, CancellationToken cancellationToken)
  {
    try
    {
      var workItem =
        await _mediator.Send(new GetWorkItemWithWorkTimeByPublicIdQuery(publicId), cancellationToken);

      if (workItem != null)
      {
        var retunValue = _mapper.Map<WorkItemRespond>(workItem);
        retunValue.WorkTimes = retunValue.WorkTimes.OrderBy(o => o.Time).ToList();

        return Ok(retunValue);
      }

      return NoContent();
    }
    catch (Exception ex)
    {
      return Problem(title: "Error Geting WorkItem", detail: ex.Message, statusCode: 500);
    }
  }
}