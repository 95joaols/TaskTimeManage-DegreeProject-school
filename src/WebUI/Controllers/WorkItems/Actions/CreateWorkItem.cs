namespace TaskTimeManage.Api.Controllers.WorkItems;

public partial class WorkItemController //NOSONAR
{
  [HttpPost]
  [Authorize]
  public async Task<ActionResult<WorkItemRespond>> CreateWorkItemAsync([FromBody] CreateWorkItemRequest request,
    CancellationToken cancellationToken)
  {
    try
    {
      var workItem = await _mediator.Send(new CreateNewWorkItemCommand(request.Name, request.UserPublicId),
        cancellationToken
      );

      return workItem != null
        ? Created("", _mapper.Map<WorkItemRespond>(workItem))
        : Problem(title: "Error Create WorkItem", detail: "Did not create WorkItem", statusCode: 500);
    }
    catch (Exception ex)
    {
      return Problem(title: "Error Create WorkItem", detail: ex.Message, statusCode: 500);
    }
  }
}