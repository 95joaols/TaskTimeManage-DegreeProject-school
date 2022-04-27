
using TaskTimeManage.Domain.Dto;
using TaskTimeManage.Domain.Entity;

namespace TaskTimeManage.Domain.Dto.Mapping;

public static class DtoMapping
{
	public static IEnumerable<WorkItemDto> ListWorkItemDtoMap(IEnumerable<WorkItem> transformFrom, Guid UserId)
	{
		List<WorkItemDto> transormed = new();
		foreach (var item in transformFrom)
		{
			transormed.Add(new WorkItemDto {
				Name = item.Name,
				PublicId = item.PublicId,
				UserId = UserId
			});
		}
		return transormed;
	}
	public static WorkItemDto WorkItemDtoMap(WorkItem transformFrom, Guid UserId)
	{
		return new WorkItemDto {
				Name = transformFrom.Name,
				PublicId = transformFrom.PublicId,
				WorkTimes = transformFrom.WorkTimes,
				UserId = UserId
			};
	}
}