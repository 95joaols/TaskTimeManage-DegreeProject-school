using Application.CQRS.WorkTimes.Commands;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TaskTimeManage.Api.Controllers.WorkTimes;

public partial class WorkTimeController
{

	[HttpDelete("{publicId:Guid}")]
	[Authorize]
	public async Task<ActionResult<bool>> DeleteWorkTimeAsync(Guid publicId, CancellationToken cancellationToken = default)
	{
		try
		{
			if (await mediator.Send(new DeleteWorkTimeByPublicIdCommand(publicId), cancellationToken))
			{

				return Ok(true);
			}
			return Problem(title: "Error");

		}
		catch (Exception ex)
		{
			return Problem(title: ex.Message, statusCode: 500);
		}
	}
}
