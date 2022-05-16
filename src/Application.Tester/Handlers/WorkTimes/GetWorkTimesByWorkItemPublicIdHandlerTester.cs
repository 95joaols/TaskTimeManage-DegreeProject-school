﻿using Application.Common.Interfaces;
using Application.CQRS.WorkTimes.Handlers;
using Application.CQRS.WorkTimes.Queries;

using Domain.Entities;

namespace Application.Handlers.WorkTimes;
public class GetWorkTimesByWorkItemPublicIdHandlerTester
{
	[Theory]
	[InlineData(1)]
	[InlineData(8)]
	[InlineData(4)]
	[InlineData(6)]
	[InlineData(7)]
	public async Task I_Can_Get_All_WorkTime_For_WorkItem(int count)
	{
		//Arrange 
		Fixture fixture = new();
		fixture.Customizations.Add(new RandomDateTimeSequenceGenerator(DateTime.Now.AddYears(-2), DateTime.Now));
		string name = fixture.Create<string>();
		IEnumerable<DateTime> times = fixture.CreateMany<DateTime>(count);

		using IApplicationDbContext dataAccess = this.CreateDataAccess();

		SetupHelper helper = new(dataAccess);
		WorkItem workItem = await helper.SetupWorkItemAsync(name);
		List<WorkTime> WorkTimes = new();

		foreach (DateTime time in times)
		{
			WorkTimes.Add(await helper.SetupWorkTimeAsync(time.ToUniversalTime(), workItem));
		}


		GetWorkTimesByWorkItemPublicIdHandler sut = new(dataAccess);
		GetWorkTimesByWorkItemPublicIdQuery request = new(workItem.PublicId);

		//Act
		IEnumerable<WorkTime>? results = await sut.Handle(request, CancellationToken.None);

		//Assert
		_ = results.Should().NotBeNullOrEmpty();
		_ = results.Should().HaveCount(count);
		_ = results.ToList().Should().BeEquivalentTo(WorkTimes, options =>
		options.ExcludingNestedObjects());
	}
}