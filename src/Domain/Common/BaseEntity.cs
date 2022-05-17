using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Common;

public class BaseEntity
{
  protected BaseEntity()
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
  public DateTimeOffset CreatedAt
  {
    get; set;
  }
  public DateTimeOffset UpdatedAt
  {
    get; set;
  }

}
