namespace Domain.Aggregates.UserAggregate;

public class UserProfile : BaseAggregate
{
  private readonly List<WorkItem> _workItems;

  private UserProfile() => _workItems = new List<WorkItem>();

  public string UserName{ get; private init; }

  public Guid IdentityId{ get; private init; }


  public IEnumerable<WorkItem> WorkItems => _workItems;

  public static UserProfile CreateUser(string userName, Guid userIdentityId)
  {
    Guard.Against.NullOrWhiteSpace(userName);
    Guard.Against.Default(userIdentityId);
    Guard.Against.NullOrWhiteSpace(userName);

    return new UserProfile {
      UserName = userName.Trim(),
      IdentityId = userIdentityId,
      CreatedAt = DateTimeOffset.Now,
      UpdatedAt = DateTimeOffset.Now
    };
  }

  public void AddWorkItem(WorkItem workItem) => _workItems.Add(workItem);

  public void RemoveWorkItem(WorkItem workItem) => _workItems.Remove(workItem);
}
