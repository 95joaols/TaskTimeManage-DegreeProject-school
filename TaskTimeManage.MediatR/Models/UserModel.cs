using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskTimeManage.MediatR.Models;
public class UserModel
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
	public string UserName
	{
		get; set;
	}
	[Required]
	public string Password
	{
		get; set;
	}
	[Required]
	public string Salt
	{
		get; set;
	}
	public List<WorkItemModel> Tasks
	{
		get; set;
	} = new();

	public DateTime CreationTime
	{
		get; set;
	}
}
