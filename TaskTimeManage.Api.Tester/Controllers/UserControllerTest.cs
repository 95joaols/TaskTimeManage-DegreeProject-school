using Microsoft.AspNetCore.Mvc;

using System.Net;

using TaskTimeManage.Api.Controllers.UserC;
using TaskTimeManage.Core.Service;
using TaskTimeManage.Domain.Context;
using TaskTimeManage.Domain.DTO;
using TaskTimeManage.Domain.Exceptions;

namespace TaskTimeManage.Api.Controllers;

public class UserControllerTest
{
	private const string username = "username";
	private const string password = "pass!03";


	[Fact]
	public async Task I_Can_Create_A_New_User()
	{
		//Arrange
		DbContextOptions<TTMDbContext>? option = this.CreatePostgreSqlUniqueMethodOptions<TTMDbContext>();
		using TTMDbContext? context = new(option);
		_ = await context.Database.EnsureDeletedAsync();
		_ = await context.Database.EnsureCreatedAsync();

		UserController sut = new(new UserService(context));

		CreateUserDTO createUserDTO = new(username, password);

		//Act
		ActionResult actionResult = await sut.CreateUserAsync(createUserDTO);
		//Assert
		var result = actionResult as CreatedResult;
		result.Should().NotBeNull();
		result.StatusCode.Should().Be((int)HttpStatusCode.Created);

	}
}
