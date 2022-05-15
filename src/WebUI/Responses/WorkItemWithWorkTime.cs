namespace WebUI.Responses;

public class WorkItemWithWorkTime
{
	public Guid PublicId
	{
		get; set;
	}
	public string Name
	{
		get; set;
	}
	public IEnumerable<WorkTimeRespons> workTimes
	{
		get; set;
	}
}
