namespace TaskTimeManage.Api.Dtos.Requsts;

public class CreateWorkItemRequsts
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
