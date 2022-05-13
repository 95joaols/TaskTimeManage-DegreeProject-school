using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TaskTimeManage.Core.Queries.WorkTimes;

namespace TaskTimeManage.Core.Handlers.WorkTimes;
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

		using TTMDataAccess dataAccess = this.CreateDataAccess();

		SetupHelper helper = new(dataAccess);
		WorkItemModel workItemModel = await helper.SetupWorkItemAsync(name);
		List<WorkTimeModel> WorkTimes = new();

		foreach (var time in times)
		{
			WorkTimes.Add(await helper.SetupWorkTimeAsync(time.ToUniversalTime(), workItemModel));
		}


		GetWorkTimesByWorkItemPublicIdHandler sut = new(dataAccess);
		GetWorkTimesByWorkItemPublicIdQuery request = new(workItemModel.PublicId);

		//Act
		var results = await sut.Handle(request, CancellationToken.None);

		//Assert
		results.Should().NotBeNullOrEmpty();
		results.Should().HaveCount(count);
		results.ToList().Should().BeEquivalentTo(WorkTimes, options =>
		options.ExcludingNestedObjects());
	}
}
