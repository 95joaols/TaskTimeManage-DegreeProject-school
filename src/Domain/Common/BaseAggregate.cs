using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Common;

public class BaseAggregate
{
  protected BaseAggregate()
  {
  }

  public int Id
  {
    get; protected set;
  }

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
