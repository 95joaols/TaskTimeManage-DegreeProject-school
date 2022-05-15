using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Common
{
	public abstract class BaseEntity<TKey>
	{
		[Key]
		public TKey Id
		{
			get; set;
		}

		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public Guid PublicId
		{
			get; set;
		}
		public DateTime? CreatedAt
		{
			get; set;
		}
		public DateTime? UpdatedAt
		{
			get; set;
		}

	}
}
