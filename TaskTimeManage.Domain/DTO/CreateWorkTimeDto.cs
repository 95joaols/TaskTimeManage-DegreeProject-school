
using TaskTimeManage.Domain.Entity;

namespace TaskTimeManage.Domain.DTO;
public class CreateWorkTimeDto
{
	public WorkTime WorkTime
	{
		get; set;
	}
	public Guid PublicId
	{
		get; set;
	}
}
