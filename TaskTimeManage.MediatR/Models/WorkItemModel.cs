using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskTimeManage.MediatR.Models;
public class WorkItemModel
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
	public UserModel User
	{
		get; set;
	}
	public List<WorkTimeModel> WorkTimes
	{
		get; set;
	} = new();
}
