using Application.Common.Interfaces;
using Application.CQRS.WorkItems.Handlers;
using Application.CQRS.WorkItems.Queries;

using Domain.Entities;

namespace Application.Handlers.WorkItems;
public class GetWorkItemByPublicIdHandlerTester
{
	[Fact]
	public async Task I_Can_Get_A_WorkItem_By_Its_PublicId()
	{
		//Arrange 
		Fixture fixture = new();
		string name = fixture.Create<string>();

		using IApplicationDbContext dataAccess = this.CreateDataAccess();

		SetupHelper helper = new(dataAccess);
		WorkItem workItem = await helper.SetupWorkItemAsync(name);


		GetWorkItemByPublicIdHandler sut = new(dataAccess);
		GetWorkItemWithWorkTimeByPublicIdQuery request = new(workItem.PublicId);

		//Act
		WorkItem? results = await sut.Handle(request, CancellationToken.None);

		//Assert
		_ = results.Should().NotBeNull();
		_ = results.Should().BeEquivalentTo(workItem);
	}
}
