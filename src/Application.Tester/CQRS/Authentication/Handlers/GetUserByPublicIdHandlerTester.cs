using Application.Common.Interfaces;
using Application.CQRS.Authentication.Queries;

using Domain.Entities;

namespace Application.CQRS.Authentication.Handlers;
public class GetUserByPublicIdHandlerTester
{
	[Fact]
	public async Task I_Can_Get_A_User()
	{
		//Arrange 
		using IApplicationDbContext dataAccess = await SetupHelper.CreateDataAccess();

		SetupHelper helper = new(dataAccess);
		User user = await helper.SetupUserAsync("Test", "Test");

		GetUserByPublicIdHandler sut = new(dataAccess);
		GetUserByPublicIdQuery request = new(user.PublicId);

		//Act 
		User? results = await sut.Handle(request, CancellationToken.None);

		//Assert
		_ = results.Should().NotBeNull();
		_ = results.Should().BeEquivalentTo(user);
	}
}
