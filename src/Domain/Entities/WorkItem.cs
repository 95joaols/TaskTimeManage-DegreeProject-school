using Domain.Common;

using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;
public sealed class WorkItem : BaseEntity
{
  [Required]
  public string Name
  {
    get; set;
  }
  [Required]
  public int UserId
  {
    get; set;
  }
  [Required]
  public User User
  {
    get; set;
  }
  public IEnumerable<WorkTime> WorkTimes
  {
    get; set;
  }
}
