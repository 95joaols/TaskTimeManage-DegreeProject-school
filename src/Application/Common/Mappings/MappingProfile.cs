using Application.Common.Models.Generated;

using AutoMapper;

using Domain.Entities;

namespace Application.Common.Mappings;

public class MappingProfile : Profile
{
  public MappingProfile()
  {
    _ = CreateMap<WorkItem, WorkItemDto>()
      .ForMember(dest => dest.workTimes, opt =>
        opt.MapFrom(src => src.WorkTimes.Select(x => new WorkTimeDto { PublicId = x.PublicId, Time = x.Time }))
      );
    _ = CreateMap<WorkTime, WorkTimeDto>();

  }
}
