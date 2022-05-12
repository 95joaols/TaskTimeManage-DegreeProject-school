using TaskTimeManage.Core.Dto;

namespace TaskTimeManage.Api.Requests;

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

