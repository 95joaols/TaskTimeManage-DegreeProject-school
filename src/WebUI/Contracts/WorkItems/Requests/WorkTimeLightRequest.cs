namespace WebUI.Contracts.WorkItems.Requests;

public class WorkTimeLightRequest
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
