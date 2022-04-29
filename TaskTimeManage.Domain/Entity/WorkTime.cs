using System.ComponentModel.DataAnnotations;

using TaskTimeManage.Domain.Enum;

namespace TaskTimeManage.Domain.Entity;

public class WorkTime
{
	public WorkTime(DateTime time, Guid publicId)
	{
		Time = time;
		PublicId = publicId;
	}

	public WorkTime()
	{
	}
	public Guid PublicId
	{
		get; set;
	}
	[Required]
	public DateTime Time
	{
		get; set;
	}
}
