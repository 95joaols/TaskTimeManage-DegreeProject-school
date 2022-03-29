using System.ComponentModel.DataAnnotations;

using TaskTimeManage.Domain.Enum;

namespace TaskTimeManage.Domain.Entity;

public class WorkTime
{
	public WorkTime(DateTime time, WorkTimeType type)
	{
		Time = time;
		Type = type;
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
	[Required]
	public WorkTimeType Type
	{
		get; set;
	}
}
