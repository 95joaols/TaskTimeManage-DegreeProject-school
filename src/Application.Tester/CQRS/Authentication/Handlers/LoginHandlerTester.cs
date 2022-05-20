using Application.Common.Service;
using Application.Common.Settings;
using Application.CQRS.Authentication.Queries;
using Application.moq;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Moq;

namespace Application.CQRS.Authentication.Handlers;

public class LoginHandlerTester
{
  [Fact]
  public async Task I_Can_Login_And_Get_A_Token()
  {
    //Arrange 
    await using ApplicationDbContextMoq dataAccess = await SetupHelper.CreateDataAccess();

    IConfigurationRoot? config = new ConfigurationBuilder()
      .SetBasePath(AppContext.BaseDirectory)
      .AddJsonFile("appsettings.json", false, true)
      .Build();

    Fixture fixture = new();
    string username = fixture.Create<string>();
    string password = fixture.Create<string>();
    JwtSettings jwtSettings = new() {
      Issuer = config.GetSection("JwtSettings:Issuer").Value,
      SigningKey = config.GetSection("JwtSettings:SigningKey").Value
    };

    IdentityUser identityUser = new() { UserName = username, PasswordHash = password };

    SetupHelper helper = new(dataAccess);
    await helper.SetupUserAsync(username, password, identityUser);

    identityUser.Id = dataAccess.UserProfile.FirstOrDefault().IdentityId.ToString();

    Mock<UserManager<IdentityUser>> userManager = SetupHelper.GetMockUserManager();

    userManager.Setup(x => x.FindByNameAsync(It.Is<string>(x => x == username))).ReturnsAsync(identityUser);

    userManager.Setup(x =>
        x.CheckPasswordAsync(It.Is<IdentityUser>(x => x == identityUser), It.Is<string>(x => x == password))
      )
      .ReturnsAsync(true);
    IdentityService identityService = new(jwtSettings);

    LoginHandler sut = new(dataAccess, userManager.Object, identityService);
    LoginQuery request = new(username, password, jwtSettings.SigningKey, jwtSettings.Issuer);

    //Act 
    string? results = await sut.Handle(request, CancellationToken.None);

    //Assert
    _ = results.Should().NotBeNullOrWhiteSpace();
  }
}