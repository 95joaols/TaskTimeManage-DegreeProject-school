using Ardalis.GuardClauses;

using Domain.Aggregates.WorkAggregate;
using Domain.Common;

namespace Domain.Aggregates.UserAggregate;
public class User : BaseAggregate
{
  private readonly List<WorkItem> workItems;

  private User()
  {
    workItems = new List<WorkItem>();
  }

  public string UserName
  {
    get; private set;
  }
  public string Password
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

  public static User CreateUser(string userName, string password, string salt)
  {
    Guard.Against.NullOrWhiteSpace(userName);
    Guard.Against.NullOrWhiteSpace(password);
    Guard.Against.NullOrWhiteSpace(salt);

    return new User {
      UserName = userName,
      Password = password,
      Salt = salt,
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
