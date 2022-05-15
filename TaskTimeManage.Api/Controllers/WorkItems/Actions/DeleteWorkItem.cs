using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using TaskTimeManage.Core.Commands.WorkItems;

namespace TaskTimeManage.Api.Controllers.WorkItems;

public partial class WorkItemController
{
	[HttpDelete("{publicId:Guid}")]
	[Authorize]
	public async Task<ActionResult<bool>> DeleteWorkItem(Guid publicId, CancellationToken cancellationToken = default)
	{
		try
		{
			if (await mediator.Send(new DeleteWorkItemCommand(publicId), cancellationToken))
			{

				return Ok(true);
			}
			else
			{
				return Problem(title: "");
			}
		}
		catch (Exception ex)
		{
			return Problem(title: ex.Message, statusCode: 500);
		}
	}
}
