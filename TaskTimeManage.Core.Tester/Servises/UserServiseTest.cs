
using FluentAssertions;

using System;

using TaskTimeManage.Domain.Context;
using TaskTimeManage.Domain.Entity;
using TaskTimeManage.Domain.Exceptions;

using Test.Helpers;

using Xunit;

namespace TaskTimeManage.Core.Servises
{
    public class UserServiseTest
    {
        const string username = "username";
        const string password = "pass!03";


        [Fact]
        public async System.Threading.Tasks.Task I_Can_Create_A_New_User()
        {
            //Arrange
            var option = this.CreatePostgreSqlUniqueMethodOptions<TTMDbContext>();
            using var context = new TTMDbContext(option);
            await context.Database.EnsureDeletedAsync();
            await context.Database.EnsureCreatedAsync();

            UserServise sut = new(context);

            //Act
            User Created = await sut.CreateUserAsync(username, password);
            //Assert
            Created.Should().NotBeNull();
            Created.Id.Should().NotBe(0);
            Created.PublicId.Should().NotBe(Guid.Empty);

        }
        [Fact]
        public async System.Threading.Tasks.Task I_Can_Login()
        {
            //Arrange
            var option = this.CreatePostgreSqlUniqueMethodOptions<TTMDbContext>();
            using var context = new TTMDbContext(option);
            await context.Database.EnsureDeletedAsync();
            await context.Database.EnsureCreatedAsync();

            UserServise sut = new(context);
            await sut.CreateUserAsync(username, password);

            //Act
            string token = await sut.Login(username, password);
            //Assert
            token.Should().NotBeNullOrWhiteSpace();

        }

        //I_Get_A_Error_If_I_Try_Create_User_Whit_Same_Name
        [Fact]
        public async System.Threading.Tasks.Task Error_Create_User_Same_Name()
        {
            //Arrange
            var option = this.CreatePostgreSqlUniqueMethodOptions<TTMDbContext>();
            using var context = new TTMDbContext(option);
            await context.Database.EnsureDeletedAsync();
            await context.Database.EnsureCreatedAsync();

            UserServise sut = new(context);
            await sut.CreateUserAsync(username, password);

            //Act
            Func<System.Threading.Tasks.Task> act = () => sut.CreateUserAsync(username, password);
            //Assert
            await act.Should().ThrowAsync<UserAlreadyExistsException>();
        }

    }
}