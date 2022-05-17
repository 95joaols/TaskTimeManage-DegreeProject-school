namespace WebUI.Contracts.WorkItems.Responds;

public class WorkTimeLightRespond
{
  public Guid PublicId
  {
    get; set;
  }
  public DateTimeOffset Time
  {
    get; set;
  }
}
