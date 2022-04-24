using System;

namespace TaskTimeManage.Domain.Dto;
public class WorkItemDto
{
	public Guid PublicId
	{
		get; set;
	}
	public string Name
	{
		get; set;
	}
	public Guid UserId
	{
		get; set;
	}
}
