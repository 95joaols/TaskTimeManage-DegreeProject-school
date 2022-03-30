
using Npgsql;

using System.Transactions;

using Xunit.Extensions.AssertExtensions;

namespace TaskTimeManage.Core;

public class DbTest
{


	[Fact]
	public async Task Test_PostgreSq_Unique_Method_Ok()
	{
		using TransactionScope? scope = new(TransactionScopeAsyncFlowOption.Enabled);


		//SETUP
		DbContextOptions<TTMDbContext>? option = this.CreatePostgreSqlUniqueMethodOptions<TTMDbContext>();
		using TTMDbContext? context = new(option);
		_ = await context.Database.EnsureCreatedAsync();


		//VERIFY
		NpgsqlConnectionStringBuilder? builder = new(
				context.Database.GetDbConnection().ConnectionString);
		builder.Database
				.ShouldEndWith($"{GetType().Name}_{nameof(Test_PostgreSq_Unique_Method_Ok)}");

	}



}
