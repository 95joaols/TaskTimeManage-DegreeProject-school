using AutoFixture;

using TaskTimeManage.Core.Queries.WorkItems;

namespace TaskTimeManage.Core.Handlers.WorkItems;
public class GetWorkItemByPublicIdHandlerTester
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


		GetWorkItemByPublicIdHandler sut = new(dataAccess);
		GetWorkItemWithWorkTimeByPublicIdQuery request = new(workItemModel.PublicId);

		//Act
		var results = await sut.Handle(request, CancellationToken.None);

		//Assert
		results.Should().NotBeNull();
		results.Should().BeEquivalentTo(workItemModel);
	}
}
