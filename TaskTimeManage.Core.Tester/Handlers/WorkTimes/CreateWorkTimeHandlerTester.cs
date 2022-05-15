using MediatR;

using Moq;

using TaskTimeManage.Core.Commands.WorkTimes;
using TaskTimeManage.Core.Queries.WorkItems;

namespace TaskTimeManage.Core.Handlers.WorkTimes;
public class CreateWorkTimeHandlerTester
{
	[Fact]
	public async Task I_Can_Create_A_New_WorkTime()
	{
		//Arrange 
		Fixture fixture = new();
		fixture.Customizations.Add(new RandomDateTimeSequenceGenerator(DateTime.Now.AddYears(-2), DateTime.Now));

		string name = fixture.Create<string>();
		DateTime time = fixture.Create<DateTime>().ToUniversalTime();


		using TTMDataAccess dataAccess = this.CreateDataAccess();


		SetupHelper helper = new(dataAccess);
		WorkItemModel workItemModel = await helper.SetupWorkItemAsync(name);

		Mock<IMediator>? mediatorMoq = new();
		_ = mediatorMoq.Setup(x => x.Send(new GetWorkItemWithWorkTimeByPublicIdQuery(workItemModel.PublicId),
		It.IsAny<CancellationToken>())).ReturnsAsync(workItemModel);

		CreateWorkTimeHandler sut = new(dataAccess, mediatorMoq.Object);
		CreateWorkTimeCommand request = new(time, workItemModel.PublicId);

		//Act
		WorkTimeModel? results = await sut.Handle(request, CancellationToken.None);

		//Assert
		_ = results.Should().NotBeNull();
		_ = results.Id.Should().NotBe(0);
		_ = results.PublicId.Should().NotBeEmpty();
		_ = results.Time.Should().Be(time);
	}
}
