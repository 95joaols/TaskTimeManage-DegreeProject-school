namespace TaskTimeManage.Api.Controllers.WorkTimes;

public partial class WorkTimeController //NOSONAR
{
  [HttpPost]
  [Authorize]
  public async Task<ActionResult<WorkTimeRespond>> CreateWorkTimeAsync([FromBody] CreateWorkTimeRequest request,
    CancellationToken cancellationToken)
  {
    try
    {
      WorkTime workItem = await _mediator.Send(new CreateWorkTimeCommand(request.Time, request.WorkItemPublicId),
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