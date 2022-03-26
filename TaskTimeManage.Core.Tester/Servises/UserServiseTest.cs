
using FluentAssertions;

using System;
using System.Threading.Tasks;

using TaskTimeManage.Domain.Exceptions;

using Xunit;

namespace TaskTimeManage.Core.Servises
{
    public class UserServiseTest
    {
        const string username = "username";
        const string password = "pass!03";


        [Fact]
        public async Task I_Can_Create_A_New_User()
        {
            //Arrange
            UserServise sut = new(new SetUp().SetUpContext());

            //Act
            bool Created = await sut.CreateUserAsync(username, password);
            //Assert
            Created.Should().BeTrue();

        }
        [Fact]
        public async Task I_Can_Login()
        {
            //Arrange
            UserServise sut = new(new SetUp().SetUpContext());
            await sut.CreateUserAsync(username, password);

            //Act
            string token = await sut.Login(username, password);
            //Assert
            token.Should().NotBeNullOrWhiteSpace();

        }

        [Fact]
        public async Task I_Get_A_Error_If_I_Try_Create_User_Whit_Same_Name()
        {
            //Arrange
            UserServise sut = new(new SetUp().SetUpContext());
            await sut.CreateUserAsync(username, password);

            //Act
            Func<Task> act = () => sut.CreateUserAsync(username, password);
            //Assert
            await act.Should().ThrowAsync<UserAlreadyExists>();
        }

    }
}