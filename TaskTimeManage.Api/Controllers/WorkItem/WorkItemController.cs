using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using TaskTimeManage.Core.Service;

namespace TaskTimeManage.Api.Controllers.WorkItem;

[Route("api/[controller]")]
[ApiController]
public partial class WorkItemController : ControllerBase
{
	private readonly UserService userService;

	private readonly WorkItemService workItemService;
	private readonly IConfiguration configuration;

	public WorkItemController(WorkItemService workItemService,UserService userService, IConfiguration configuration)
	{
		this.workItemService = workItemService;
		this.configuration = configuration;
		this.userService = userService;
	}
}
