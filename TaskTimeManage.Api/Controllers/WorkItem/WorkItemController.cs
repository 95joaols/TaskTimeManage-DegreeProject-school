using Microsoft.AspNetCore.Mvc;

using TaskTimeManage.Core.Service;

namespace TaskTimeManage.Api.Controllers.WorkItem;

[Route("api/[controller]")]
[ApiController]
public partial class WorkItemController : ControllerBase
{
	private readonly UserService userService;

	private readonly WorkItemService workItemService;
	private readonly WorkTimeService workTimeService;
	private readonly IConfiguration configuration;

	public WorkItemController(WorkItemService workItemService, WorkTimeService workTimeService, UserService userService, IConfiguration configuration)
	{
		this.workItemService = workItemService;
		this.workTimeService = workTimeService;
		this.configuration = configuration;
		this.userService = userService;
	}
}
