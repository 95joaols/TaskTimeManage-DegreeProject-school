using FluentAssertions;

using System.Threading.Tasks;

using TaskTimeManage.Domain.Context;
using TaskTimeManage.Domain.Entity;

using Xunit;

namespace TaskTimeManage.Core.Servises
{
    public class TaskServiseTest
    {
        const string username = "username";
        const string password = "pass!03";


        [Fact]
        public async System.Threading.Tasks.Task I_can_create_a_new_task()
        {
            //Arrange
            TTMDbContext Context = new SetUp().SetUpContext();
            UserServise userServise = new(Context);
            await userServise.CreateUserAsync(username, password);
            User? user = await userServise.GetUserByNameAsync(username);
            Assert.NotNull(user);

            TaskServise sut = new(Context);

            //Act
            Domain.Entity.Task task = await sut.CreateTaskAsync("name of task", user);

            //Assert
            task.Should().NotBeNull();
            task.Id.Should().NotBe(0);

        }

        [Fact]
        public async System.Threading.Tasks.Task I_Can_Get_All_Task_From_User()
        {
            //Arrange
            TTMDbContext Context = new SetUp().SetUpContext();
            UserServise userServise = new(Context);
            await userServise.CreateUserAsync(username, password);
            User? user = await userServise.GetUserByNameAsync(username);
            Assert.NotNull(user);

            TaskServise sut = new(Context);
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