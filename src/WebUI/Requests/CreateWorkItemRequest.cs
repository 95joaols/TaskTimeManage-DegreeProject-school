namespace WebUI.Requests;

public class CreateWorkItemRequest
{
	public string Name
	{
		get; set;
	}
	public Guid UserPublicId
	{
		get; set;
	}
}
