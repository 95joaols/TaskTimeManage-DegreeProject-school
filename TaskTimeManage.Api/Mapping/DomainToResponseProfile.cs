using AutoMapper;

using TaskTimeManage.Api.Responses;
using TaskTimeManage.Core.Models;

namespace TaskTimeManage.Api.Mapping;

public class DomainToResponseProfile : Profile
{
	public DomainToResponseProfile()
	{
		CreateMap<WorkItemModel, WorkItemRespons>();
		CreateMap<WorkItemModel, WorkItemWithWorkTime>()
			.ForMember(dest => dest.workTimes, opt =>
				opt.MapFrom(src => src.WorkTimes.Select(x => new WorkTimeRespons { PublicId = x.PublicId, Time = x.Time }))
			);
		CreateMap<WorkTimeModel, WorkTimeRespons>();

	}
}
