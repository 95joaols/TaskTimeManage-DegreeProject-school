﻿using AutoFixture;

using TaskTimeManage.Core.Queries.WorkItems;

namespace TaskTimeManage.Core.Handlers.WorkItems;
public class GetWorkItemByUserPublicIdHandlerTester
{
	[Theory]
	[InlineData(1)]
	[InlineData(8)]
	[InlineData(4)]
	[InlineData(6)]
	[InlineData(7)]

	public async Task I_Can_Get_All_WorkItem_For_A_User(int count)
	{
		//Arrange 
		Fixture fixture = new();
		IEnumerable<string> names = fixture.CreateMany<string>(count);
		string username = fixture.Create<string>();
		string password = fixture.Create<string>();
		using TTMDataAccess dataAccess = this.CreateDataAccess();
		List<WorkItemModel> workItems = new();


		SetupHelper helper = new(dataAccess);
		UserModel userModel = await helper.SetupUserAsync(username, password);
		foreach (var name in names)
		{
			workItems.Add(await helper.SetupWorkItemAsync(name, userModel));
		}
		GetWorkItemByUserPublicIdHandler sut = new(dataAccess);
		GetWorkItemTimeByUserPublicIdQuery request = new(userModel.PublicId);

		//Act
		var results = await sut.Handle(request, CancellationToken.None);

		//Assert
		results.Should().NotBeNullOrEmpty();
		results.Should().HaveCount(count);
		results.ToList().Should().BeEquivalentTo(workItems, options =>
		options.ExcludingNestedObjects());
	}
}
