namespace WebUI.Contracts.WorkItems.Responds;

public class WorkItemRespond
{
  public Guid PublicId{ get; set; }

  public string Name{ get; set; }

  public IEnumerable<WorkTimeLightRespond> WorkTimes{ get; set; }
}