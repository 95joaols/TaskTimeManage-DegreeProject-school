using Ardalis.GuardClauses;

using Domain.Aggregates.WorkAggregate;
using Domain.Common;

namespace Domain.Aggregates.UserAggregate;
public class UserProfile : BaseAggregate
{
  private readonly List<WorkItem> workItems;

  private UserProfile()
  {
    workItems = new List<WorkItem>();
  }

  public string UserName
  {
    get; private set;
  }
  public string IdentityId
  {
    get; private set;
  }
  public string HashedPassword
  {
    get; private set;
  }
  public string Salt
  {
    get; private set;
  }
  public IEnumerable<WorkItem> WorkItems
  {
    get => workItems;
  }

  public static UserProfile CreateUser(string userName,string UserIdentityId, string hashedPassword, string salt)
  {
    Guard.Against.NullOrWhiteSpace(userName);
    Guard.Against.NullOrWhiteSpace(UserIdentityId);
    Guard.Against.NullOrWhiteSpace(hashedPassword);
    Guard.Against.NullOrWhiteSpace(userName);

    return new UserProfile {
      UserName = userName.Trim(),
      IdentityId = UserIdentityId,
      HashedPassword = hashedPassword.Trim(),
      Salt = salt.Trim(),
      CreatedAt = DateTimeOffset.Now,
      UpdatedAt = DateTimeOffset.Now,
    };
  }

  public void AddWorkItem(WorkItem workItem)
  {
    workItems.Add(workItem);
  }
  public void RemoveWorkItem(WorkItem workItem)
  {
    workItems.Remove(workItem);
  }
}
