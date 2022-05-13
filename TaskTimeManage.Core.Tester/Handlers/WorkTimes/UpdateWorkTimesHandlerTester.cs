using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TaskTimeManage.Core.Commands.WorkTimes;
using TaskTimeManage.Core.Dto;

namespace TaskTimeManage.Core.Handlers.WorkTimes;
public class UpdateWorkTimesHandlerTester
{
	[Theory]
	[InlineData(1)]
	[InlineData(8)]
	[InlineData(4)]
	[InlineData(6)]
	[InlineData(7)]
	public async Task I_Can_Update_List_Of_WorkTime(int count)
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
		List<WorkTimesLight> toUpdate = new();
		foreach (var WorkTime in WorkTimes)
		{
			toUpdate.Add(new() {
				PublicId = WorkTime.PublicId,
				time = fixture.Create<DateTime>().ToUniversalTime()

			});
			;
		}


		UpdateWorkTimesHandler sut = new(dataAccess);
		UpdateWorkTimesCommand request = new(toUpdate);

		//Act
		var results = await sut.Handle(request, CancellationToken.None);

		//Assert
		results.Should().NotBeNullOrEmpty();
		results.Should().HaveCount(count);
		foreach (var result in results)
		{
			result.Time.Should().Be(toUpdate.FirstOrDefault(x => x.PublicId == result.PublicId).time);
		}
	}
}
