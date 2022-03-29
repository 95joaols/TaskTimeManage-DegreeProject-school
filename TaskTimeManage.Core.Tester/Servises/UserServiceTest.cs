
using System.Transactions;

using TaskTimeManage.Core.Service;
using TaskTimeManage.Domain.Exceptions;

namespace TaskTimeManage.Core.Servises;

public class UserServiceTest
{
	private const string username = "username";
	private const string password = "pass!03";


	[Fact]
	public async Task I_Can_Create_A_New_User()
	{
		using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

		//Arrange
		DbContextOptions<TTMDbContext>? option = this.CreatePostgreSqlUniqueMethodOptions<TTMDbContext>();
		using TTMDbContext? context = new(option);
		_ = await context.Database.EnsureDeletedAsync();
		_ = await context.Database.EnsureCreatedAsync();

		UserService sut = new(context);

		//Act
		User Created = await sut.CreateUserAsync(username, password, default);
		//Assert
		_ = Created.Should().NotBeNull();
		_ = Created.Id.Should().NotBe(0);
		_ = Created.PublicId.Should().NotBe(Guid.Empty);

	}
	[Fact]
	public async Task I_Can_Login()
	{
		using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

		//Arrange
		DbContextOptions<TTMDbContext>? option = this.CreatePostgreSqlUniqueMethodOptions<TTMDbContext>();
		using TTMDbContext? context = new(option);
		_ = await context.Database.EnsureDeletedAsync();
		_ = await context.Database.EnsureCreatedAsync();

		UserService sut = new(context);
		_ = await sut.CreateUserAsync(username, password, default);

		//Act
		string token = await sut.LoginAsync(username, password, default);
		//Assert
		_ = token.Should().NotBeNullOrWhiteSpace();

	}

	//I_Get_A_Error_If_I_Try_Create_User_Whit_Same_Name
	[Fact]
	public async Task Error_Create_User_Same_Name()
	{
		using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

		//Arrange
		DbContextOptions<TTMDbContext>? option = this.CreatePostgreSqlUniqueMethodOptions<TTMDbContext>();
		using TTMDbContext? context = new(option);
		_ = await context.Database.EnsureDeletedAsync();
		_ = await context.Database.EnsureCreatedAsync();

		UserService sut = new(context);
		_ = await sut.CreateUserAsync(username, password, default);

		//Act
		Func<Task> act = () => sut.CreateUserAsync(username, password, default);
		//Assert
		_ = await act.Should().ThrowAsync<UserAlreadyExistsException>();
	}

}
