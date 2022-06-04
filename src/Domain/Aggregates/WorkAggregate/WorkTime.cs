using System.Text.Json.Serialization;

namespace Domain.Aggregates.WorkAggregate;

public class WorkTime
{
  private WorkTime() {}

  /// <summary>
  /// Do not use this (This is only for JsonConstructor)
  /// </summary>
  [JsonConstructor]
  public WorkTime(DateTimeOffset time, Guid publicId, DateTimeOffset createdAt, DateTimeOffset updatedAt)
  {
    Time = time;
    PublicId = publicId;
    CreatedAt = createdAt;
    UpdatedAt = updatedAt;
  }

  public DateTimeOffset Time{ get; private set; }
  public Guid PublicId{ get; private init; }

  public DateTimeOffset CreatedAt{ get; private init; }

  public DateTimeOffset UpdatedAt{ get; private set; }

  public static WorkTime CreateWorkTime(DateTimeOffset time)
  {
    Guard.Against.Default(time);

    if (time > DateTimeOffset.Now)
    {
      time = DateTimeOffset.Now;
    }

    return new WorkTime {
      PublicId = Guid.NewGuid(),
      Time = time,
      CreatedAt = DateTimeOffset.Now,
      UpdatedAt = DateTimeOffset.Now
    };
  }

  public static WorkTime CreateWorkTime(Guid publicId, DateTimeOffset time)
  {
    Guard.Against.Default(time);
    Guard.Against.Default(publicId);

    if (time > DateTimeOffset.Now)
    {
      time = DateTimeOffset.Now;
    }

    return new WorkTime {
      PublicId = publicId,
      Time = time,
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