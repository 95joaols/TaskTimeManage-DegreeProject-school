using System.ComponentModel.DataAnnotations;

using TaskTimeManage.Domain.Enum;

namespace TaskTimeManage.Domain.Entity;

public class WorkTime
{
	public WorkTime(DateTime time)
	{
		Time = time;
	}

	public WorkTime()
	{
	}
	[Key]
	public int Id
	{
		get; set;
	}
	[Required]
	public DateTime Time
	{
		get; set;
	}
}
