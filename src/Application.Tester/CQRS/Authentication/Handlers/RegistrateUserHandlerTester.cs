using Application.Common.Exceptions;
using Application.CQRS.Authentication.Commands;
using Microsoft.AspNetCore.Identity;
using Moq;

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
    await using var dataAccess = this.CreateDataAccess();

    Mock<UserManager<IdentityUser>> userManager = SetupHelper.GetMockUserManager();
    IdentityUser identityUser = new() {
      Id = Guid.NewGuid().ToString(),
      UserName = username,
      PasswordHash = password
    };

    userManager.SetupSequence(x => x.FindByNameAsync(It.Is<string>(x => x == username)))
      .ReturnsAsync((IdentityUser)null).ReturnsAsync(identityUser);


    userManager.Setup(x =>
      x.CreateAsync(It.IsAny<IdentityUser>(), It.Is<string>(x => x == password))
    ).ReturnsAsync(IdentityResult.Success);

    userManager.Setup(x =>
      x.CheckPasswordAsync(It.IsAny<IdentityUser>(), It.Is<string>(x => x == password))
    ).ReturnsAsync(true);

    RegistrateUserHandler sut = new(dataAccess, userManager.Object);
    RegistrateUserCommand request = new(username, password);
    //Act 
    var user = await sut.Handle(request, CancellationToken.None);
    //Assert 
    user.Should().NotBeNull();
    user.Id.Should().NotBe(0);
    user.PublicId.Should().NotBeEmpty();
    user.UserName.Should().Be(username);
  }

  [Theory]
  [InlineData("fgfgr", "dghf779j&35")]
  [InlineData("fghdtry5", "dghfhbdtfrby779j&35")]
  [InlineData("54ytyhg", "dghf779ntfyysj&35")]
  [InlineData("zdfhgr5sy", "ybdruyu&35")]
  [InlineData("test", "test")]
  [InlineData("tdryntry5edynt", "hdtrydnnund5gb&35")]
  [InlineData("fgfdtygr", "dghffgbhdrt774e779j&35")]
  public async Task I_Cant_Create_Multiple_On_Same_Name(string username, string password)
  {
    //Arrange
    await using var dataAccess = this.CreateDataAccess();
    Mock<UserManager<IdentityUser>> userManager = SetupHelper.GetMockUserManager();

    IdentityUser identityUser = new() {
      Id = Guid.NewGuid().ToString(),
      UserName = username,
      PasswordHash = password
    };
    userManager.SetupSequence(x => x.FindByNameAsync(It.Is<string>(x => x == username)))
      .ReturnsAsync((IdentityUser)null).ReturnsAsync(identityUser).ReturnsAsync(identityUser);


    userManager.Setup(x =>
      x.CreateAsync(It.IsAny<IdentityUser>(), It.Is<string>(x => x == password))
    ).ReturnsAsync(IdentityResult.Success);

    userManager.Setup(x =>
      x.CheckPasswordAsync(It.IsAny<IdentityUser>(), It.Is<string>(x => x == password))
    ).ReturnsAsync(true);


    RegistrateUserHandler sut = new(dataAccess, userManager.Object);

    RegistrateUserCommand request = new(username, password);
    await sut.Handle(request, CancellationToken.None);

    //Act
    //Assert
    await sut.Invoking(s => s.Handle(request, CancellationToken.None)).Should()
      .ThrowAsync<UserAlreadyExistsException>();
  }
}