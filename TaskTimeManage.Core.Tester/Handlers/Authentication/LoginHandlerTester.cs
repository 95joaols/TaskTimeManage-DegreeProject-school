using Microsoft.Extensions.Configuration;

using TaskTimeManage.Core.Queries.Authentication;

namespace TaskTimeManage.Core.Handlers.Authentication;
public class LoginHandlerTester
{
	[Fact]
	public async Task I_Can_Login_And_Get_A_Token()
	{
		//Arrange 
		using TTMDataAccess dataAccess = this.CreateDataAccess();

		IConfigurationRoot? config = new ConfigurationBuilder()
		.SetBasePath(AppContext.BaseDirectory)
		.AddJsonFile("appsettings.json", false, true)
		.Build();

		Fixture fixture = new();
		string username = fixture.Create<string>();
		string password = fixture.Create<string>();
		string tokenKey = config.GetSection("AppSettings:Token").Value;

		SetupHelper helper = new(dataAccess);
		UserModel userModel = await helper.SetupUserAsync(username, password);

		LoginHandler sut = new(dataAccess);
		LoginQuery request = new(username, password, tokenKey);

		//Act 
		string? results = await sut.Handle(request, CancellationToken.None);

		//Assert
		_ = results.Should().NotBeNullOrWhiteSpace();
	}
}
