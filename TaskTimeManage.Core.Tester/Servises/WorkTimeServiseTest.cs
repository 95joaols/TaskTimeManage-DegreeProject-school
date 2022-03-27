using FluentAssertions;

using Microsoft.EntityFrameworkCore;

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
        private const string username = "username";
        private const string password = "pass!03";


        [Fact]
        public async System.Threading.Tasks.Task I_can_create_a_new_WorkTime()
        {
            //Arrange
            DbContextOptions<TTMDbContext>? option = this.CreatePostgreSqlUniqueMethodOptions<TTMDbContext>();
            using TTMDbContext? context = new(option);
            _ = await context.Database.EnsureDeletedAsync();
            _ = await context.Database.EnsureCreatedAsync();


            UserServise userServise = new(context);
            User user = await userServise.CreateUserAsync(username, password);


            WorkItemServise taskServise = new(context);
            WorkItem task = await taskServise.CreateTaskAsync("name of task", user);
            Assert.NotNull(task);

            WorkTimeServise sut = new(context);

            //Act
            WorkTime workTime = await sut.CreateWorkTimeAsync(DateTime.Now, WorkTimeType.Start, task);

            //Assert
            _ = workTime.Should().NotBeNull();
            _ = task.WorkTimes.Should().HaveCount(1);

        }
    }
}