using Domain.Common;

using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;
public sealed class User : BaseEntity<int>
{
  [Required]
  public string UserName
  {
    get; set;
  }
  [Required]
  public string Password
  {
    get; set;
  }
  [Required]
  public string Salt
  {
    get; set;
  }
  public List<WorkItem> Tasks
  {
    get; set;
  } = new();
}
