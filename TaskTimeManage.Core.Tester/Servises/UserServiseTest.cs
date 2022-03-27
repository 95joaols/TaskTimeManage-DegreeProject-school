
using FluentAssertions;

using Microsoft.EntityFrameworkCore;

using System;
using System.Threading.Tasks;

using TaskTimeManage.Domain.Context;
using TaskTimeManage.Domain.Entity;
using TaskTimeManage.Domain.Exceptions;

using Test.Helpers;

using Xunit;

namespace TaskTimeManage.Core.Servises
{
    public class UserServiseTest
    {
        private const string username = "username";
        private const string password = "pass!03";


        [Fact]
        public async Task I_Can_Create_A_New_User()
        {
            //Arrange
            DbContextOptions<TTMDbContext>? option = this.CreatePostgreSqlUniqueMethodOptions<TTMDbContext>();
            using TTMDbContext? context = new(option);
            _ = await context.Database.EnsureDeletedAsync();
            _ = await context.Database.EnsureCreatedAsync();

            UserServise sut = new(context);

            //Act
            User Created = await sut.CreateUserAsync(username, password);
            //Assert
            _ = Created.Should().NotBeNull();
            _ = Created.Id.Should().NotBe(0);
            _ = Created.PublicId.Should().NotBe(Guid.Empty);

        }
        [Fact]
        public async Task I_Can_Login()
        {
            //Arrange
            DbContextOptions<TTMDbContext>? option = this.CreatePostgreSqlUniqueMethodOptions<TTMDbContext>();
            using TTMDbContext? context = new(option);
            _ = await context.Database.EnsureDeletedAsync();
            _ = await context.Database.EnsureCreatedAsync();

            UserServise sut = new(context);
            _ = await sut.CreateUserAsync(username, password);

            //Act
            string token = await sut.Login(username, password);
            //Assert
            _ = token.Should().NotBeNullOrWhiteSpace();

        }

        //I_Get_A_Error_If_I_Try_Create_User_Whit_Same_Name
        [Fact]
        public async Task Error_Create_User_Same_Name()
        {
            //Arrange
            DbContextOptions<TTMDbContext>? option = this.CreatePostgreSqlUniqueMethodOptions<TTMDbContext>();
            using TTMDbContext? context = new(option);
            _ = await context.Database.EnsureDeletedAsync();
            _ = await context.Database.EnsureCreatedAsync();

            UserServise sut = new(context);
            _ = await sut.CreateUserAsync(username, password);

            //Act
            Func<Task> act = () => sut.CreateUserAsync(username, password);
            //Assert
            _ = await act.Should().ThrowAsync<UserAlreadyExistsException>();
        }

    }
}