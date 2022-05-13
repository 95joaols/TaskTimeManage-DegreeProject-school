using MediatR;

using Microsoft.EntityFrameworkCore;

using Moq;

using TaskTimeManage.Core.Commands.WorkItems;
using TaskTimeManage.Core.Commands.WorkTimes;

namespace TaskTimeManage.Core.Handlers.WorkItems;
public class DeleteWorkItemHandlerTester
{
	[Fact]
	public async Task I_Can_Delete_A_WorkItem()
	{
		//Arrange 
		Fixture fixture = new();
		string name = fixture.Create<string>();

		using TTMDataAccess dataAccess = this.CreateDataAccess();

		SetupHelper helper = new(dataAccess);
		WorkItemModel workItemModel = await helper.SetupWorkItemAsync(name);

		Mock<IMediator>? mediatorMoq = new Mock<IMediator>();
		mediatorMoq.Setup(x => x.Send(new DeleteAllWorkTimesByWorkItemIdCommand(workItemModel.Id),
		It.IsAny<CancellationToken>())).ReturnsAsync(true);

		DeleteWorkItemHandler sut = new(dataAccess, mediatorMoq.Object);
		DeleteWorkItemCommand request = new(workItemModel.PublicId);

		//Act
		bool results = await sut.Handle(request, CancellationToken.None);

		//Assert
		results.Should().BeTrue();
		(await dataAccess.WorkItem.AnyAsync(x => x.Id == workItemModel.Id)).Should().BeFalse();

	}
	[Fact]
	public async Task It_Will_Stopp_If_It_Resive_False()
	{
		//Arrange 
		Fixture fixture = new();
		string name = fixture.Create<string>();

		using TTMDataAccess dataAccess = this.CreateDataAccess();

		SetupHelper helper = new(dataAccess);
		WorkItemModel workItemModel = await helper.SetupWorkItemAsync(name);

		Mock<IMediator>? mediatorMoq = new Mock<IMediator>();
		mediatorMoq.Setup(x => x.Send(new DeleteAllWorkTimesByWorkItemIdCommand(workItemModel.Id),
		It.IsAny<CancellationToken>())).ReturnsAsync(false);

		DeleteWorkItemHandler sut = new(dataAccess, mediatorMoq.Object);
		DeleteWorkItemCommand request = new(workItemModel.PublicId);

		//Act
		//Assert
		await sut.Invoking(s => s.Handle(request, CancellationToken.None)).Should().ThrowAsync<Exception>();
	}

}
