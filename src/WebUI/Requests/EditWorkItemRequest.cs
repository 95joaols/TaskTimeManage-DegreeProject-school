using TaskTimeManage.Core.Dto;

namespace WebUI.Requests;

public class EditWorkItemRequest
{
	public Guid PublicId
	{
		get; set;
	}
	public string Name
	{
		get; set;
	}
	public IEnumerable<WorkTimesLight>? WorkTimes
	{
		get; set;
	}
}

