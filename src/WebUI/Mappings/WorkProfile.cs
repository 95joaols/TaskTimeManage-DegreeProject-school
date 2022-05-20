namespace WebUI.Mappings;

public class WorkProfile : Profile
{
  public WorkProfile()
  {
    CreateMap<WorkItem, WorkItemRespond>()
      .ForMember(dest => dest.WorkTimes,
        opt =>
          opt.MapFrom(src => src.WorkTimes.Select(x => new WorkTimeLightRespond(x.PublicId,x.Time)
            )
          )
      );
    CreateMap<WorkTime, WorkTimeRespond>();
  }
}