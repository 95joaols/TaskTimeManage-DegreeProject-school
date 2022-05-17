using Ardalis.GuardClauses;
using Domain.Aggregates.WorkAggregate;
using Domain.Common;

namespace Domain.Aggregates.UserAggregate;

public class UserProfile : BaseAggregate
{
  private readonly List<WorkItem> _workItems;

  private UserProfile() => _workItems = new List<WorkItem>();

  public string UserName{ get; private set; }

  public string IdentityId{ get; private set; }

  public string HashedPassword{ get; private set; }

  public string Salt{ get; private set; }

  public IEnumerable<WorkItem> WorkItems => _workItems;

  public static UserProfile CreateUser(string userName, string userIdentityId, string hashedPassword, string salt)
  {
    Guard.Against.NullOrWhiteSpace(userName);
    Guard.Against.NullOrWhiteSpace(userIdentityId);
    Guard.Against.NullOrWhiteSpace(hashedPassword);
    Guard.Against.NullOrWhiteSpace(userName);

    return new UserProfile {
      UserName = userName.Trim(),
      IdentityId = userIdentityId,
      HashedPassword = hashedPassword.Trim(),
      Salt = salt.Trim(),
      CreatedAt = DateTimeOffset.Now,
      UpdatedAt = DateTimeOffset.Now
    };
  }

  public void AddWorkItem(WorkItem workItem) => _workItems.Add(workItem);

  public void RemoveWorkItem(WorkItem workItem) => _workItems.Remove(workItem);
}