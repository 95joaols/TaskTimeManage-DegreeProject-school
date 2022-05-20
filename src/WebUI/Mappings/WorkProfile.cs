namespace WebUI.Mappings;

public class WorkProfile : Profile
{
  public WorkProfile()
  {
    _ = CreateMap<WorkItem, WorkItemRespond>()
      .ForMember(dest => dest.WorkTimes, opt =>
        opt.MapFrom(src => src.WorkTimes.Select(x => new WorkTimeLightRespond { PublicId = x.PublicId, Time = x.Time }))
      );
    _ = CreateMap<WorkTime, WorkTimeRespond>();
  }
}