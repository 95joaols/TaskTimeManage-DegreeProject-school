using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskTimeManage.Core.Models;
public class WorkTimeModel
{
	[Key]
	public int Id
	{
		get; set;
	}
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public Guid PublicId
	{
		get; set;
	}
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
	public WorkItemModel WorkItem
	{
		get; set;
	}
}
