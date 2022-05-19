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


    SetupHelper helper = new(dataAccess);
    await helper.SetupUserAsync(username, password);

    Mock<UserManager<IdentityUser>> userManager = SetupHelper.GetMockUserManager(username, password);
    var IdentityService = new IdentityService(jwtSettings);

    LoginHandler sut = new(dataAccess, userManager.Object, IdentityService);
    LoginQuery request = new(username, password, jwtSettings.SigningKey, jwtSettings.Issuer);

    //Act 
    string? results = await sut.Handle(request, CancellationToken.None);

    //Assert
    _ = results.Should().NotBeNullOrWhiteSpace();
  }
}