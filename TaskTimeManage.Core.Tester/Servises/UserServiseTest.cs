
using FluentAssertions;

using TaskTimeManage.Core.Security;
using TaskTimeManage.Domain.Context;
using TaskTimeManage.Domain.Entity;

using TestSupport.EfHelpers;

using Xunit;

namespace TaskTimeManage.Core.Servises
{
    public class UserServiseTest
    {
        const string username = "username";
        const string password = "pass!03";
        const string salt = "$2a$11$BfZDWZ58tnozI.PsoJPl8O";


        [Fact]
        public async void I_Can_Create_A_New_User()
        {
            //Arrange
            UserServise sut = new(SetUpContext());
            string hashedPassword = Cryptography.Encrypt(Cryptography.Hash(Cryptography.Encrypt(password, salt), salt), salt);

            //Act
            bool Created = await sut.CreateUserAsync(new User(username, hashedPassword, salt));
            //Assert
            Created.Should().BeTrue();

        }
        [Fact]
        public async void I_Can_Login()
        {
            //Arrange
            UserServise sut = new(SetUpContext());
            string hashedPassword = Cryptography.Encrypt(Cryptography.Hash(Cryptography.Encrypt(password, salt), salt), salt);

            await sut.CreateUserAsync(new User(username, hashedPassword, salt));

            //Act
            string token = await sut.Login(username, password);
            //Assert
            token.Should().NotBeNullOrWhiteSpace();

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