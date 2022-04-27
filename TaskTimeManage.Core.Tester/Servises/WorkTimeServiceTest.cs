
using System.Transactions;

using TaskTimeManage.Core.Service;
using TaskTimeManage.Domain.Enum;

namespace TaskTimeManage.Core.Servises;

public class WorkTimeServiceTest
{
	private const string username = "username";
	private const string password = "pass!03";


	[Fact]
	public async Task I_can_create_a_new_WorkTime()
	{
		using TransactionScope? scope = new(TransactionScopeAsyncFlowOption.Enabled);

		//Arrange
		DbContextOptions<TTMDbContext>? option = this.CreatePostgreSqlUniqueMethodOptions<TTMDbContext>();
		using TTMDbContext? context = new(option);
		_ = await context.Database.EnsureDeletedAsync();
		_ = await context.Database.EnsureCreatedAsync();


		UserService userServise = new(context);
		User user = await userServise.CreateUserAsync(username, password, default);


		WorkItemService taskServise = new(context);
		WorkItem task = await taskServise.CreateTaskAsync("name of task", user, default);
		Assert.NotNull(task);

		WorkTimeService sut = new(context);

		//Act
		WorkTime workTime = await sut.CreateWorkTimeAsync(DateTime.Now, task, default);

		//Assert
		_ = workTime.Should().NotBeNull();
		_ = task.WorkTimes.Should().HaveCount(1);

	}
}
