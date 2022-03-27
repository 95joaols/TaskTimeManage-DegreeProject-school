using FluentAssertions;

using TaskTimeManage.Domain.Context;
using TaskTimeManage.Domain.Entity;

using Test.Helpers;

using Xunit;

namespace TaskTimeManage.Core.Servises
{
    public class WorkItemServiseTest
    {
        const string username = "username";
        const string password = "pass!03";


        [Fact]
        public async System.Threading.Tasks.Task I_can_create_a_new_task()
        {
            //Arrange
            var option = this.CreatePostgreSqlUniqueMethodOptions<TTMDbContext>();
            using var context = new TTMDbContext(option);
            await context.Database.EnsureDeletedAsync();
            await context.Database.EnsureCreatedAsync();


            UserServise userServise = new(context);
            User user = await userServise.CreateUserAsync(username, password);

            WorkItemServise sut = new(context);

            //Act
            Domain.Entity.WorkItem task = await sut.CreateTaskAsync("name of task", user);

            //Assert
            task.Should().NotBeNull();
            task.Id.Should().NotBe(0);

        }

        [Fact]
        public async System.Threading.Tasks.Task I_Can_Get_All_Task_From_User()
        {
            //Arrange
            var option = this.CreatePostgreSqlUniqueMethodOptions<TTMDbContext>();
            using var context = new TTMDbContext(option);
            await context.Database.EnsureDeletedAsync();
            await context.Database.EnsureCreatedAsync();


            UserServise userServise = new(context);
            User user = await userServise.CreateUserAsync(username, password);
            Assert.NotNull(user);

            WorkItemServise sut = new(context);
            //Act
            await sut.CreateTaskAsync("name of task1", user);
            await sut.CreateTaskAsync("name of task2", user);
            await sut.CreateTaskAsync("name of task3", user);
            await sut.CreateTaskAsync("name of task4", user);
            await sut.CreateTaskAsync("name of task5", user);
            await sut.CreateTaskAsync("name of task6", user);


            //Assert
            user.Tasks.Should().HaveCount(6);
        }

    }
}