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

    }
}