

using TaskTimeManage.Core.Queries.Authentication;

namespace TaskTimeManage.Core.Handlers.Authentication;
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
		results.Should().NotBeNull();
		results.Should().BeEquivalentTo(userModel);
	}
}
