using Ardalis.GuardClauses;

using Domain.Aggregates.UserAggregate;
using Domain.Common;

namespace Domain.Aggregates.WorkAggregate;
public class WorkItem : BaseAggregate
{
  private readonly List<WorkTime> workTimes;

  private WorkItem()
  {
    workTimes = new List<WorkTime>();
  }

  public string Name
  {
    get; private set;
  }
  public int UserId
  {
    get; private set;
  }
  public User User
  {
    get; private set;
  }
  public IEnumerable<WorkTime> WorkTimes => workTimes;

  public static WorkItem CreateUser(string name, User user)
  {
    Guard.Against.NullOrWhiteSpace(name);
    Guard.Against.Null(user);


    return new WorkItem {
      Name = name,
      User = user,
      UserId = user.Id,
      CreatedAt = DateTimeOffset.Now,
      UpdatedAt = DateTimeOffset.Now,
    };
  }

  public void AddWorkTime(WorkTime workItem)
  {
    workTimes.Add(workItem);
  }
  public void RemoveWorkTime(WorkTime workItem)
  {
    workTimes.Remove(workItem);
  }

  public void UpdateName(string name)
  {
    Name = name;
    UpdatedAt = DateTimeOffset.Now;
  }
}
