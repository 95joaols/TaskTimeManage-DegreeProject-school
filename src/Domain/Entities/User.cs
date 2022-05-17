using Domain.Common;

using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;
public sealed class User : BaseEntity
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
  public IEnumerable<WorkItem> WorkItems
  {
    get; set;
  }
}
