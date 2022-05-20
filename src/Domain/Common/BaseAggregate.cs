namespace Domain.Common;

public class BaseAggregate
{
  protected BaseAggregate() {}

  public int Id{ get; protected set; }

  public Guid PublicId{ get; protected init; }

  public DateTimeOffset CreatedAt{ get; protected init; }

  public DateTimeOffset UpdatedAt{ get; protected set; }
}