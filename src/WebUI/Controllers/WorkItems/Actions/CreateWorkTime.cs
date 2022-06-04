namespace TaskTimeManage.Api.Controllers.WorkItems;

public partial class WorkItemController //NOSONAR
{
  [HttpPost("{workItemPublicId:Guid}/WorkTime")]
  [Authorize]
  public async Task<ActionResult<WorkTimeRespond>> CreateWorkTimeAsync(Guid workItemPublicId, [FromBody] CreateWorkTimeRequest request,
    CancellationToken cancellationToken)
  {
    try
    {
      var workItem = await _mediator.Send(new CreateWorkTimeCommand(request.Time, workItemPublicId),
        cancellationToken
      );
      if (workItem == null)
      {
        return BadRequest();
      }

      return Created("", _mapper.Map<WorkTimeRespond>(workItem));
    }
    catch (Exception ex)
    {
      return Problem(title: "Error Creating WorkTime", detail: ex.Message, statusCode: 500);
    }
  }
}