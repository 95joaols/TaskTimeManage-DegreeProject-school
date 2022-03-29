using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskTimeManage.Domain.Entity
{
	public class WorkItem
	{
		public WorkItem(string name, User user)
		{
			Name = name;
			User = user;
		}

		public WorkItem()
		{

		}
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
		public User User
		{
			get; set;
		}
		[Column(TypeName = "jsonb")]
		public List<WorkTime> WorkTimes
		{
			get; set;
		} = new();

	}
}
