
using TaskTimeManage.Domain.Entity;

using TestSupport.EfHelpers;

using Xunit;
using FluentAssertions;
using TaskTimeManage.Domain.Context;

namespace TaskTimeManage.Core.Servises
{
    public class UserServiseTest
    {

        [Fact]
        public async void I_Can_Create_A_New_User()
        {
            //Arrange
            UserServise sut = new(SetUpContext());

            //Act
            bool Created = await sut.CreateUserAsync(new User("username","pass!03"));
            //Assert
            Created.Should().BeTrue();

        }

        private static TTMDbContext SetUpContext()
        {
            var option = SqliteInMemory.CreateOptions<TTMDbContext>();
            var context = new TTMDbContext(option);
            context.Database.EnsureCreated();
            return context;
        }
    }
}