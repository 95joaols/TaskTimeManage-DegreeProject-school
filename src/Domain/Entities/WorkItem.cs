using Domain.Common;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;
public sealed class WorkItem : BaseEntity<int>
{
	[Required]
	public string Name
	{
		get; set;
	}
	[Required]
	public int UserId
	{
		get; set;
	}
	[Required]
	public User User
	{
		get; set;
	}
	public List<WorkTime> WorkTimes
	{
		get; set;
	} = new();
}
