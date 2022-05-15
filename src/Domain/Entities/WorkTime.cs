using Domain.Common;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;
public sealed class WorkTime : BaseEntity<int>
{
	[Required]
	public DateTime Time
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
