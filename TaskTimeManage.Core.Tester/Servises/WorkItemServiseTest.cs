using FluentAssertions;

using Microsoft.EntityFrameworkCore;

using System.Threading.Tasks;

using TaskTimeManage.Domain.Context;
using TaskTimeManage.Domain.Entity;

using Test.Helpers;

using Xunit;

namespace TaskTimeManage.Core.Servises
{
    public class WorkItemServiseTest
    {
        private const string username = "username";
        private const string password = "pass!03";


        [Fact]
        public async Task I_can_create_a_new_task()
        {
            //Arrange
            DbContextOptions<TTMDbContext>? option = this.CreatePostgreSqlUniqueMethodOptions<TTMDbContext>();
            using TTMDbContext? context = new(option);
            _ = await context.Database.EnsureDeletedAsync();
            _ = await context.Database.EnsureCreatedAsync();


            UserServise userServise = new(context);
            User user = await userServise.CreateUserAsync(username, password);

            WorkItemServise sut = new(context);

            //Act
            WorkItem task = await sut.CreateTaskAsync("name of task", user);

            //Assert
            _ = task.Should().NotBeNull();
            _ = task.Id.Should().NotBe(0);

        }

        [Fact]
        public async Task I_Get_All_Task_From_User()
        {
            //Arrange
            DbContextOptions<TTMDbContext>? option = this.CreatePostgreSqlUniqueMethodOptions<TTMDbContext>();
            using TTMDbContext? context = new(option);
            _ = await context.Database.EnsureDeletedAsync();
            _ = await context.Database.EnsureCreatedAsync();


            UserServise userServise = new(context);
            User user = await userServise.CreateUserAsync(username, password);
            Assert.NotNull(user);

            WorkItemServise sut = new(context);
            //Act
            _ = await sut.CreateTaskAsync("name of task1", user);
            _ = await sut.CreateTaskAsync("name of task2", user);
            _ = await sut.CreateTaskAsync("name of task3", user);
            _ = await sut.CreateTaskAsync("name of task4", user);
            _ = await sut.CreateTaskAsync("name of task5", user);
            _ = await sut.CreateTaskAsync("name of task6", user);


            //Assert
            _ = user.Tasks.Should().HaveCount(6);
        }

    }
}