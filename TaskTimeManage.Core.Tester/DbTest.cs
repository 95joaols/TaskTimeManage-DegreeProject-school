using Microsoft.EntityFrameworkCore;

using Npgsql;

using Xunit.Extensions.AssertExtensions;

namespace TaskTimeManage.Core.Tester;

public class DbTest
{
	[Fact]
	public void TestPostgreSqlUniqueClassOk()
	{
		//Arrange 
		//Act
		using TTMDataAccess dataAccess = this.CreateDataAccess();

		//Assert
		NpgsqlConnectionStringBuilder? builder = new NpgsqlConnectionStringBuilder(
				dataAccess.Database.GetDbConnection().ConnectionString);
		builder.Database.ShouldEndWith(GetType().Name);
	}
}