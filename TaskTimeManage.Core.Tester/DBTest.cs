
using Microsoft.EntityFrameworkCore;

using Npgsql;

using System.Threading.Tasks;

using TaskTimeManage.Domain.Context;

using Test.Helpers;

using Xunit;
using Xunit.Extensions.AssertExtensions;

namespace TaskTimeManage.Core
{
	public class DbTest
	{


		[Fact]
		public async Task Test_PostgreSq_Unique_Method_Ok()
		{

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
}