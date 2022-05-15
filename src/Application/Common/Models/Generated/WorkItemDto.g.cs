namespace Application.Common.Models.Generated;

public class WorkItemDto
{
	public Guid PublicId
	{
		get; set;
	}
	public string Name
	{
		get; set;
	}
	public IEnumerable<WorkTimeDto> workTimes
	{
		get; set;
	}
}
