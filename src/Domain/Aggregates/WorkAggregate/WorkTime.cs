namespace Domain.Aggregates.WorkAggregate;

public class WorkTime : BaseAggregate
{
  private WorkTime()
  {
  }

  public DateTimeOffset Time{ get; private set; }

  public int WorkItemId{ get; private set; }

  public WorkItem WorkItem{ get; private init; }

  public static WorkTime CreateWorkTime(DateTimeOffset time, WorkItem workItem)
  {
    Guard.Against.Default(time);
    Guard.Against.Null(workItem);

    if (time > DateTimeOffset.Now)
    {
      time = DateTimeOffset.Now;
    }

    return new WorkTime {
      Time = time, WorkItem = workItem, CreatedAt = DateTimeOffset.Now, UpdatedAt = DateTimeOffset.Now
    };
  }

  public static WorkTime CreateWorkTime(Guid publicId, DateTimeOffset time, WorkItem workItem)
  {
    Guard.Against.Default(time);
    Guard.Against.Default(publicId);
    Guard.Against.Null(workItem);

    if (time > DateTimeOffset.Now)
    {
      time = DateTimeOffset.Now;
    }

    return new WorkTime {
      PublicId = publicId,
      Time = time,
      WorkItem = workItem,
      CreatedAt = DateTimeOffset.Now,
      UpdatedAt = DateTimeOffset.Now
    };
  }

  public void UpdateTime(DateTimeOffset time)
  {
    Guard.Against.Default(time);

    Time = time;
    UpdatedAt = DateTimeOffset.Now;
  }
}