﻿namespace TaskTimeManage.Api.Requests;

public class CreateWorkTimeRequest
{
	public DateTime Time
	{
		get; set;
	}
	public Guid WorkItemPublicId
	{
		get; set;
	}
}