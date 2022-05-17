using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.CQRS.Authentication.Commands;

using Domain.Entities;

namespace Application.CQRS.Authentication.Handlers;
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
    using IApplicationDbContext dataAccess = await SetupHelper.CreateDataAccess();

    RegistrateUserHandler sut = new(dataAccess);
    RegistrateUserCommand request = new(username, password);
    //Act 
    User? user = await sut.Handle(request, CancellationToken.None);
    //Assert 
    _ = user.Should().NotBeNull();
    _ = user.Id.Should().NotBe(0);
    _ = user.PublicId.Should().NotBeEmpty();
    _ = user.UserName.Should().Be(username);
    _ = user.Password.Should().NotBe(password);

  }

  [Fact]
  public async Task I_Cant_Create_Multiple_On_Same_Name()
  {
    //Arrange
    using IApplicationDbContext? dataAccess = await SetupHelper.CreateDataAccess();

    RegistrateUserHandler sut = new(dataAccess);

    RegistrateUserCommand request = new("Test", "Test");
    _ = await sut.Handle(request, CancellationToken.None);

    //Act
    //Assert
    _ = await sut.Invoking(s => s.Handle(request, CancellationToken.None)).Should().ThrowAsync<UserAlreadyExistsException>();

  }
}
