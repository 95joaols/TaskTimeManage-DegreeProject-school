using FluentAssertions;

using System;
using System.Threading.Tasks;

using TaskTimeManage.Domain.Context;
using TaskTimeManage.Domain.Entity;
using TaskTimeManage.Domain.Enum;

using Test.Helpers;

using TestSupport.EfHelpers;

using Xunit;

namespace TaskTimeManage.Core.Servises
{
    public class WorkTimeServiseTest
    {
        const string username = "username";
        const string password = "pass!03";


        [Fact]
        public async System.Threading.Tasks.Task I_can_create_a_new_WorkTime()
        {
            //Arrange
            var option = this.CreatePostgreSqlUniqueMethodOptions<TTMDbContext>();
            using var context = new TTMDbContext(option);
            await context.Database.EnsureDeletedAsync();
            await context.Database.EnsureCreatedAsync();


            UserServise userServise = new(context);
            await userServise.CreateUserAsync(username, password);
            User? user = await userServise.GetUserByNameAsync(username);
            Assert.NotNull(user);

            TaskServise taskServise = new(context);
            Domain.Entity.Task task = await taskServise.CreateTaskAsync("name of task", user);
            Assert.NotNull(task);

            WorkTimeServise sut = new(context);

            //Act
            WorkTime workTime = await sut.CreateWorkTimeAsync(DateTime.Now, WorkTimeType.Start, task);

            //Assert
            workTime.Should().NotBeNull();
            task.WorkTimes.Should().HaveCount(1);

        }
    }
}