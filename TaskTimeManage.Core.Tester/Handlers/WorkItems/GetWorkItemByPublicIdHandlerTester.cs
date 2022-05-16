﻿using TaskTimeManage.Core.Queries.WorkItems;

namespace TaskTimeManage.Core.Handlers.WorkItems;
public class GetWorkItemByPublicIdHandlerTester
{
	[Fact]
	public async Task I_Can_Get_A_WorkItem_By_Its_PublicId()
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
		WorkItemModel? results = await sut.Handle(request, CancellationToken.None);

		//Assert
		_ = results.Should().NotBeNull();
		_ = results.Should().BeEquivalentTo(workItemModel);
	}
}