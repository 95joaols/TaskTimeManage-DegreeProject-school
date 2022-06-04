namespace TaskTimeManage.Api.Controllers.WorkItems;

public partial class WorkItemController //NOSONAR
{
  [HttpDelete("{WorkItemId:Guid}/WorkTime/{publicId:Guid}")]
  [Authorize]
  public async Task<ActionResult<bool>> DeleteWorkTimeAsync(Guid workItemId,Guid publicId, CancellationToken cancellationToken)
  {
    try
    {
      if (await _mediator.Send(new DeleteWorkTimeByPublicIdCommand(workItemId, publicId), cancellationToken))
      {
        return Ok(true);
      }

      return Problem(title: "Error");
    }
    catch (Exception ex)
    {
      return Problem(title: "Error delete WorkTime", detail: ex.Message, statusCode: 500);
    }
  }
}