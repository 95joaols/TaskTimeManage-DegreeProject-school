namespace TaskTimeManage.Api.Controllers.WorkItems;

[Route("api/[controller]")]
[ApiController]
public partial class WorkItemController : ControllerBase //NOSONAR
{
  private readonly IMapper _mapper;
  private readonly IMediator _mediator;

  public WorkItemController(IMediator mediator, IMapper mapper)
  {
    _mediator = mediator;
    _mapper = mapper;
  }
}