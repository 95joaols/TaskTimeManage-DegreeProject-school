using MediatR;

using Moq;

using TaskTimeManage.Core.Commands.WorkItems;
using TaskTimeManage.Core.Queries.Authentication;

namespace TaskTimeManage.Core.Handlers.WorkItems;
public class CreateNewWorkItemHandlerTester
{
	[Fact]
	public async Task I_Can_Create_A_New_WorkItem()
	{
		//Arrange 
		Fixture fixture = new();
		string name = fixture.Create<string>();
		string username = fixture.Create<string>();
		string password = fixture.Create<string>();

		using TTMDataAccess dataAccess = this.CreateDataAccess();


		SetupHelper helper = new(dataAccess);
		UserModel userModel = await helper.SetupUserAsync(username, password);

		Mock<IMediator>? mediatorMoq = new Mock<IMediator>();
		mediatorMoq.Setup(x => x.Send(new GetUserByPublicIdQuery(userModel.PublicId),
		It.IsAny<CancellationToken>())).ReturnsAsync(userModel);

		CreateNewWorkItemHandler sut = new(dataAccess, mediatorMoq.Object);
		CreateNewWorkItemCommand request = new(name, userModel.PublicId);

		//Act
		var results = await sut.Handle(request, CancellationToken.None);

		//Assert
		results.Should().NotBeNull();
		results.Id.Should().NotBe(0);
		results.PublicId.Should().NotBeEmpty();
		results.Name.Should().Be(name);
	}
}
