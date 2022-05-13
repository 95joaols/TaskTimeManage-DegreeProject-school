using Microsoft.EntityFrameworkCore;

using TaskTimeManage.Core.Commands.WorkTimes;

namespace TaskTimeManage.Core.Handlers.WorkTimes;
public class DeleteWorkTimeByPublicIdHandlerTester
{
	[Fact]
	public async Task I_Can_Delete_WorkTime_By_Its_PublicId()
	{
		//Arrange 
		Fixture fixture = new();
		fixture.Customizations.Add(new RandomDateTimeSequenceGenerator(DateTime.Now.AddYears(-2), DateTime.Now));
		string name = fixture.Create<string>();
		DateTime time = fixture.Create<DateTime>();

		using TTMDataAccess dataAccess = this.CreateDataAccess();

		SetupHelper helper = new(dataAccess);
		WorkTimeModel workTimeModel = await helper.SetupWorkTimeAsync(time.ToUniversalTime());
		

		DeleteWorkTimeByPublicIdHandler sut = new(dataAccess);
		DeleteWorkTimeByPublicIdCommand request = new(workTimeModel.PublicId);

		//Act
		bool results = await sut.Handle(request, CancellationToken.None);

		//Assert
		results.Should().BeTrue();
		(await dataAccess.WorkTime.AnyAsync(x => x.Id == workTimeModel.Id)).Should().BeFalse();
	}
}
