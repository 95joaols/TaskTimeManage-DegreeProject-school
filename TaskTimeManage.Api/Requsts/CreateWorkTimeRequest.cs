namespace TaskTimeManage.Api.Dtos.Requsts;

public class CreateWorkTimeRequest
{
	public DateTime Time
	{
		get; set;
	}
	public Guid WorkItemPublicId
	{
		get; set;
	}
}
