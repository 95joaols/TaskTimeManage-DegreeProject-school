using AutoMapper;

using TaskTimeManage.Api.Dtos.Responses;
using TaskTimeManage.MediatR.Models;

namespace TaskTimeManage.Api.Dtos.Mapping;

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
