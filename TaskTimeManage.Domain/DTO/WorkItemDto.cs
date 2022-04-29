using System;
using System.Collections.Generic;
using TaskTimeManage.Domain.Entity;

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
	public List<WorkTime>? WorkTimes
	{
		get; set;
	}
}
