using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

using System.Net;
using System.Transactions;

using TaskTimeManage.Api.Controllers.User;
using TaskTimeManage.Core.Service;
using TaskTimeManage.Domain.Context;
using TaskTimeManage.Domain.Dto;

namespace TaskTimeManage.Api.Controllers;

public class UserControllerTest
{
	private const string username = "username";
	private const string password = "pass!03";


	[Fact]
	public async Task I_Can_Create_A_New_User()
	{
		using TransactionScope? scope = new(TransactionScopeAsyncFlowOption.Enabled);

		//Arrange
		Dictionary<string, string>? inMemorySettings = new() {
		{"AppSettings:Token", "GCuwl/Jf8ob5vNDTSsrorORpr81X5FV818rnsvhRfK1KqJ/xobg6M9VCxjVyGGbxnO0LwsI5IjLrbogshFVXTg=="}
		};

		IConfiguration configuration = new ConfigurationBuilder()
				.AddInMemoryCollection(initialData: inMemorySettings)
				.Build();

		DbContextOptions<TTMDbContext>? option = this.CreatePostgreSqlUniqueMethodOptions<TTMDbContext>();
		using TTMDbContext? context = new(option);
		_ = await context.Database.EnsureDeletedAsync();
		_ = await context.Database.EnsureCreatedAsync();

		UserController sut = new(new UserService(context), configuration);

		UserDto createUserDTO = new(username, password);

		//Act
		ActionResult actionResult = await sut.CreateUserAsync(createUserDTO);
		//Assert
		CreatedResult? result = actionResult as CreatedResult;
		_ = result.Should().NotBeNull();
		_ = result.StatusCode.Should().Be((int)HttpStatusCode.Created);

	}
}
