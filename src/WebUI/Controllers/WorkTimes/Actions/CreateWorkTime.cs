using Application.Common.Models.Generated;
using Application.CQRS.WorkTimes.Commands;

using Domain.Entities;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using WebUI.Requests;

namespace TaskTimeManage.Api.Controllers.WorkTimes;

public partial class WorkTimeController //NOSONAR
{

  [HttpPost]
  [Authorize]
  public async Task<ActionResult<WorkTimeDto>> CreateWorkTimeAsync([FromBody] CreateWorkTimeRequest request, CancellationToken cancellationToken)
  {
    try
    {
      WorkTime workItem = await mediator.Send(new CreateWorkTimeCommand(request.Time, request.WorkItemPublicId), cancellationToken);
      if (workItem == null)
      {
        return BadRequest();
      }

      return Created("", mapper.Map<WorkTimeDto>(workItem));
    }
    catch (Exception ex)
    {
      return Problem(title: ex.Message, statusCode: 500);
    }
  }
}
