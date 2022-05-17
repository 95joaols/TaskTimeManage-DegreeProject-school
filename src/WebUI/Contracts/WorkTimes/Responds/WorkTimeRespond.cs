namespace WebUI.Contracts.WorkTimes.Responds;

public class WorkTimeRespond
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
