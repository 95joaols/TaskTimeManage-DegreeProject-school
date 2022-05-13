using TaskTimeManage.Core.Commands.Authentication;
using TaskTimeManage.Core.Exceptions;

namespace TaskTimeManage.Core.Handlers.Authentication;
public class RegistrateUserHandlerTester
{
	[Theory]
	[InlineData("fgfgr", "dghf779j&35")]
	[InlineData("fghdtry5", "dghfhbdtfrby779j&35")]
	[InlineData("54ytyhg", "dghf779ntfyysj&35")]
	[InlineData("zdfhgr5sy", "ybdruyu&35")]
	[InlineData("test", "test")]
	[InlineData("tdryntry5edynt", "hdtrydnnund5gb&35")]
	[InlineData("fgfdtygr", "dghffgbhdrt774e779j&35")]

	public async Task I_Can_Registrate_A_New_User(string username, string password)
	{
		//Arrange 
		using TTMDataAccess dataAccess = this.CreateDataAccess();

		RegistrateUserHandler sut = new(dataAccess);
		RegistrateUserCommand request = new(username, password);
		//Act 
		var userModel = await sut.Handle(request, CancellationToken.None);
		//Assert 
		userModel.Should().NotBeNull();
		userModel.Id.Should().NotBe(0);
		userModel.PublicId.Should().NotBeEmpty();
		userModel.UserName.Should().Be(username);
		userModel.Password.Should().NotBe(password);

	}

	[Fact]
	public async Task I_Cant_Create_Multiple_On_Same_Name()
	{
		//Arrange
		using TTMDataAccess? dataAccess = this.CreateDataAccess();

		RegistrateUserHandler sut = new(dataAccess);

		RegistrateUserCommand request = new("Test", "Test");
		await sut.Handle(request, CancellationToken.None);

		//Act
		//Assert
		await sut.Invoking(s => s.Handle(request, CancellationToken.None)).Should().ThrowAsync<UserAlreadyExistsException>();

	}
}
