using Domain.Aggregates.UserAggregate;

namespace Domain.Aggregates.WorkAggregate;

public class WorkItem : BaseAggregate
{
  private readonly List<WorkTime> _workTimes;

  private WorkItem() => _workTimes = new List<WorkTime>();

  public string Name{ get; private set; }
  public UserProfile User{ get; private init; }
  public IEnumerable<WorkTime> WorkTimes => _workTimes;

  public static WorkItem CreateWorkItem(string name, UserProfile user)
  {
    Guard.Against.NullOrWhiteSpace(name);
    Guard.Against.Null(user);


    return new WorkItem {
      Name = name.Trim(),
      User = user,
      CreatedAt = DateTimeOffset.Now,
      UpdatedAt = DateTimeOffset.Now
    };
  }
  public void UpdateName(string name)
  {
    Guard.Against.NullOrWhiteSpace(name);

    Name = name.Trim();
    UpdatedAt = DateTimeOffset.Now;
  }

  public void AddWorkTimes(WorkTime workTime)
  {
    _workTimes.Add(workTime);
  }

  public void RemoveWorkTime(Guid deletePublicId)
  {
    _workTimes.Remove(_workTimes.FirstOrDefault(x => x.PublicId == deletePublicId));
  }
}