using Microsoft.EntityFrameworkCore;

using TaskTimeManage.Core.Commands.WorkTimes;

namespace TaskTimeManage.Core.Handlers.WorkTimes;
public class DeleteAllWorkTimesByWorkItemIdHandlerTester
{
	[Theory]
	[InlineData(1)]
	[InlineData(8)]
	[InlineData(4)]
	[InlineData(6)]
	[InlineData(7)]
	public async Task I_Can_Delete_All_WorkTime_For_WorkItem(int count)
	{
		//Arrange 
		Fixture fixture = new();
		fixture.Customizations.Add(new RandomDateTimeSequenceGenerator(DateTime.Now.AddYears(-2), DateTime.Now));

		string name = fixture.Create<string>();
		IEnumerable<DateTime> times = fixture.CreateMany<DateTime>(count);

		using TTMDataAccess dataAccess = this.CreateDataAccess();

		SetupHelper helper = new(dataAccess);
		WorkItemModel workItemModel = await helper.SetupWorkItemAsync(name);
		foreach (DateTime time in times)
		{
			_ = await helper.SetupWorkTimeAsync(time.ToUniversalTime(), workItemModel);
		}

		DeleteAllWorkTimesByWorkItemIdHandler sut = new(dataAccess);
		DeleteAllWorkTimesByWorkItemIdCommand request = new(workItemModel.Id);

		//Act
		bool results = await sut.Handle(request, CancellationToken.None);

		//Assert
		_ = results.Should().BeTrue();
		_ = (await dataAccess.WorkTime.AnyAsync(x => x.WorkItemId == workItemModel.Id)).Should().BeFalse();
	}
}
