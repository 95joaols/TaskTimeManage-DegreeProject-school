namespace TaskTimeManage.Api.Controllers.WorkItems;

public partial class WorkItemController //NOSONAR
{
  [HttpPut("{publicId:guid}")]
  [Authorize]
  public async Task<ActionResult<WorkItemRespond>> EditWorkItemAsync(Guid publicId,
    [FromBody] EditWorkItemRequest request,
    CancellationToken token)
  {
    try
    {
      var workItem =
        await _mediator.Send(new UpdateWorkItemCommand(publicId, request.Name), token);
      if (request.WorkTimes.Any())
      {

        await _mediator.Send(
          new UpdateWorkTimesCommand(publicId,
            request.WorkTimes.Select(x =>WorkTime.CreateWorkTime(x.PublicId, x.Time))
          ),
          token
        );
      }

      return workItem != null
        ? Ok(_mapper.Map<WorkItemRespond>(workItem))
        : Problem(title: "Error Edit WorkItem", detail: "Did not Edit the WorkItem", statusCode: 500);
    }
    catch (Exception ex)
    {
      return Problem(title: "Error Edit WorkItem", detail: ex.Message, statusCode: 500);
    }
  }
}