using Application.Common.Settings;
using Application.CQRS.Authentication.Queries;
using Application.moq;
using Application.Common.Service;


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
    using ApplicationDbContextMoq dataAccess = await SetupHelper.CreateDataAccess();

    IConfigurationRoot? config = new ConfigurationBuilder()
      .SetBasePath(AppContext.BaseDirectory)
      .AddJsonFile("appsettings.json", false, true)
      .Build();

    Fixture fixture = new();
    string username = fixture.Create<string>();
    string password = fixture.Create<string>();
    JwtSettings jwtSettings = new();
    jwtSettings.Issuer = config.GetSection("JwtSettings:Issuer").Value;
    jwtSettings.SigningKey = config.GetSection("JwtSettings:SigningKey").Value;

    IdentityUser identityUser = new();
    identityUser.UserName = username;
    identityUser.PasswordHash = password;

    SetupHelper helper = new(dataAccess);
    await helper.SetupUserAsync(username, password, identityUser);

    identityUser.Id = dataAccess.UserProfile.FirstOrDefault().IdentityId.ToString();

    Mock<UserManager<IdentityUser>> userManager = SetupHelper.GetMockUserManager();

    userManager.Setup(x => x.FindByNameAsync(It.Is<string>(x => x == username))).ReturnsAsync(identityUser);

    userManager.Setup(x =>
    x.CheckPasswordAsync(It.Is<IdentityUser>(x => x == identityUser), It.Is<string>(x => x == password))).ReturnsAsync(true);
    var IdentityService = new IdentityService(jwtSettings);

    LoginHandler sut = new(dataAccess, userManager.Object, IdentityService);
    LoginQuery request = new(username, password, jwtSettings.SigningKey, jwtSettings.Issuer);

    //Act 
    string? results = await sut.Handle(request, CancellationToken.None);

    //Assert
    _ = results.Should().NotBeNullOrWhiteSpace();
  }
}