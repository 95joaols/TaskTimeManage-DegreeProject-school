using Domain.Common;

using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;
public sealed class WorkTime : BaseEntity<int>
{
	[Required]
	public DateTimeOffset Time
	{
		get; set;
	}

	[Required]
	public int WorkItemId
	{
		get; set;
	}

	[Required]
	public WorkItem WorkItem
	{
		get; set;
	}
}
