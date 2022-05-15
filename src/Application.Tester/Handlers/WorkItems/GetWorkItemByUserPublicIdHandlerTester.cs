using Application.Common.Interfaces;
using Application.CQRS.WorkItems.Handlers;
using Application.CQRS.WorkItems.Queries;

using Domain.Entities;

namespace Application.Handlers.WorkItems;
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
		using IApplicationDbContext dataAccess = this.CreateDataAccess();
		List<WorkItem> workItems = new();


		SetupHelper helper = new(dataAccess);
		User user = await helper.SetupUserAsync(username, password);
		foreach (string? name in names)
		{
			workItems.Add(await helper.SetupWorkItemAsync(name, user));
		}
		GetWorkItemByUserPublicIdHandler sut = new(dataAccess);
		GetWorkItemTimeByUserPublicIdQuery request = new(user.PublicId);

		//Act
		IEnumerable<WorkItem>? results = await sut.Handle(request, CancellationToken.None);

		//Assert
		_ = results.Should().NotBeNullOrEmpty();
		_ = results.Should().HaveCount(count);
		_ = results.ToList().Should().BeEquivalentTo(workItems, options =>
		options.ExcludingNestedObjects());
	}
}
