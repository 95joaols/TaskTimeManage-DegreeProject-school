using FluentAssertions;

using System;

using TaskTimeManage.Domain.Context;
using TaskTimeManage.Domain.Entity;
using TaskTimeManage.Domain.Enum;

using Test.Helpers;

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
            User user = await userServise.CreateUserAsync(username, password);


            WorkItemServise taskServise = new(context);
            Domain.Entity.WorkItem task = await taskServise.CreateTaskAsync("name of task", user);
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