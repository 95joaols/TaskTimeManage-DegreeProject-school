using Application;
using Application.Commands.Authentication;
using Application.DataAccess;
using Application.Exceptions;
using Application.Models;

namespace Application.Handlers.Authentication;
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
		UserModel? userModel = await sut.Handle(request, CancellationToken.None);
		//Assert 
		_ = userModel.Should().NotBeNull();
		_ = userModel.Id.Should().NotBe(0);
		_ = userModel.PublicId.Should().NotBeEmpty();
		_ = userModel.UserName.Should().Be(username);
		_ = userModel.Password.Should().NotBe(password);

	}

	[Fact]
	public async Task I_Cant_Create_Multiple_On_Same_Name()
	{
		//Arrange
		using TTMDataAccess? dataAccess = this.CreateDataAccess();

		RegistrateUserHandler sut = new(dataAccess);

		RegistrateUserCommand request = new("Test", "Test");
		_ = await sut.Handle(request, CancellationToken.None);

		//Act
		//Assert
		_ = await sut.Invoking(s => s.Handle(request, CancellationToken.None)).Should().ThrowAsync<UserAlreadyExistsException>();

	}
}
