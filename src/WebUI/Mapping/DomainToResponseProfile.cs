using AutoMapper;

using TaskTimeManage.Core.Models;

using WebUI.Responses;

namespace WebUI.Mapping;

public class DomainToResponseProfile : Profile
{
	public DomainToResponseProfile()
	{
		_ = CreateMap<WorkItemModel, WorkItemRespons>();
		_ = CreateMap<WorkItemModel, WorkItemWithWorkTime>()
			.ForMember(dest => dest.workTimes, opt =>
				opt.MapFrom(src => src.WorkTimes.Select(x => new WorkTimeRespons { PublicId = x.PublicId, Time = x.Time }))
			);
		_ = CreateMap<WorkTimeModel, WorkTimeRespons>();

	}
}
