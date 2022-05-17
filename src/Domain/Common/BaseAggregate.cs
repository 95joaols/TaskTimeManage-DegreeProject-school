using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Common;

public class BaseAggregate
{
  protected BaseAggregate()
  {
  }

  [Key]
  public int Id
  {
    get; protected set;
  }

  [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
  public Guid PublicId
  {
    get; protected set;
  }
  public DateTimeOffset CreatedAt
  {
    get; protected set;
  }
  public DateTimeOffset UpdatedAt
  {
    get; protected set;
  }

}
