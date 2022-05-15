using Application;
using Application.Commands.WorkItems;
using Application.DataAccess;
using Application.Models;

using MediatR;

using Microsoft.EntityFrameworkCore;

using Moq;

using TaskTimeManage.Core.Commands.WorkTimes;

namespace Application.Handlers.WorkItems;
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

		Mock<IMediator>? mediatorMoq = new();
		_ = mediatorMoq.Setup(x => x.Send(new DeleteAllWorkTimesByWorkItemIdCommand(workItemModel.Id),
		It.IsAny<CancellationToken>())).ReturnsAsync(true);

		DeleteWorkItemHandler sut = new(dataAccess, mediatorMoq.Object);
		DeleteWorkItemCommand request = new(workItemModel.PublicId);

		//Act
		bool results = await sut.Handle(request, CancellationToken.None);

		//Assert
		_ = results.Should().BeTrue();
		_ = (await dataAccess.WorkItem.AnyAsync(x => x.Id == workItemModel.Id)).Should().BeFalse();

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

		Mock<IMediator>? mediatorMoq = new();
		_ = mediatorMoq.Setup(x => x.Send(new DeleteAllWorkTimesByWorkItemIdCommand(workItemModel.Id),
		It.IsAny<CancellationToken>())).ReturnsAsync(false);

		DeleteWorkItemHandler sut = new(dataAccess, mediatorMoq.Object);
		DeleteWorkItemCommand request = new(workItemModel.PublicId);

		//Act
		//Assert
		_ = await sut.Invoking(s => s.Handle(request, CancellationToken.None)).Should().ThrowAsync<Exception>();
	}

}
