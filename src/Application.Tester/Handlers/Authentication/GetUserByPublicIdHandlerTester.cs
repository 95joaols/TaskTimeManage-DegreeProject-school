

using Application;
using Application.DataAccess;
using Application.Models;
using Application.Queries.Authentication;

namespace Application.Handlers.Authentication;
public class GetUserByPublicIdHandlerTester
{
	[Fact]
	public async Task I_Can_Get_A_User()
	{
		//Arrange 
		using TTMDataAccess dataAccess = this.CreateDataAccess();

		SetupHelper helper = new(dataAccess);
		UserModel userModel = await helper.SetupUserAsync("Test", "Test");

		GetUserByPublicIdHandler sut = new(dataAccess);
		GetUserByPublicIdQuery request = new(userModel.PublicId);

		//Act 
		UserModel? results = await sut.Handle(request, CancellationToken.None);

		//Assert
		_ = results.Should().NotBeNull();
		_ = results.Should().BeEquivalentTo(userModel);
	}
}
