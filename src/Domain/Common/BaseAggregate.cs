namespace Domain.Common;

public class BaseAggregate
{
  protected BaseAggregate() {}

  public int Id{ get; protected init; }

  public Guid PublicId{ get; protected init; }

  public DateTimeOffset CreatedAt{ get; protected init; }

  public DateTimeOffset UpdatedAt{ get; protected set; }
}