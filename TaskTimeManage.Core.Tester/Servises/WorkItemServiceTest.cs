using System.Transactions;

using TaskTimeManage.Core.Service;

namespace TaskTimeManage.Core.Servises;

public class WorkItemServiceTest
{
	private const string username = "username";
	private const string password = "pass!03";


	[Fact]
	public async Task I_can_create_a_new_task()
	{
		using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

		//Arrange
		DbContextOptions<TTMDbContext>? option = this.CreatePostgreSqlUniqueMethodOptions<TTMDbContext>();
		using TTMDbContext? context = new(option);
		_ = await context.Database.EnsureDeletedAsync();
		_ = await context.Database.EnsureCreatedAsync();


		UserService userServise = new(context);
		User user = await userServise.CreateUserAsync(username, password, default);

		WorkItemService sut = new(context);

		//Act
		WorkItem task = await sut.CreateTaskAsync("name of task", user, default);

		//Assert
		_ = task.Should().NotBeNull();
		_ = task.Id.Should().NotBe(0);

	}

	[Fact]
	public async Task I_Get_All_Task_From_User()
	{
		using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

		//Arrange
		DbContextOptions<TTMDbContext>? option = this.CreatePostgreSqlUniqueMethodOptions<TTMDbContext>();
		using TTMDbContext? context = new(option);
		_ = await context.Database.EnsureDeletedAsync();
		_ = await context.Database.EnsureCreatedAsync();


		UserService userServise = new(context);
		User user = await userServise.CreateUserAsync(username, password, default);
		Assert.NotNull(user);

		WorkItemService sut = new(context);
		//Act
		_ = await sut.CreateTaskAsync("name of task1", user, default);
		_ = await sut.CreateTaskAsync("name of task2", user, default);
		_ = await sut.CreateTaskAsync("name of task3", user, default);
		_ = await sut.CreateTaskAsync("name of task4", user, default);
		_ = await sut.CreateTaskAsync("name of task5", user, default);
		_ = await sut.CreateTaskAsync("name of task6", user, default);


		//Assert
		_ = user.Tasks.Should().HaveCount(6);
	}

}
