using Microsoft.EntityFrameworkCore;

using Npgsql;

using Xunit.Extensions.AssertExtensions;

namespace TaskTimeManage.Core.Tester;

public class DbTest
{
	[Fact]
	public void TestPostgreSqlUniqueClassOk()
	{
		//SETUP
		//ATTEMPT
		DbContextOptions<TTMDataAccess>? options = this.CreatePostgreSqlUniqueClassOptions<TTMDataAccess>();
		using TTMDataAccess? context = new TTMDataAccess(options);
		//VERIFY
		NpgsqlConnectionStringBuilder? builder = new NpgsqlConnectionStringBuilder(
				context.Database.GetDbConnection().ConnectionString);
		builder.Database.ShouldEndWith(GetType().Name);
	}
}