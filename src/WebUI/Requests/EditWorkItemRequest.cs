using Application.Common.Models.Generated;

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
	public IEnumerable<WorkTimeDto>? WorkTimes
	{
		get; set;
	}
}

