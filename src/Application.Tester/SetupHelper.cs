﻿using Application.CQRS.Authentication.Commands;
using Application.CQRS.Authentication.Handlers;
using Application.CQRS.Authentication.Queries;
using Application.CQRS.WorkItems.Commands;
using Application.CQRS.WorkItems.Handlers;
using Application.CQRS.WorkItems.Queries;
using Application.CQRS.WorkTimes.Commands;
using Application.CQRS.WorkTimes.Handlers;
using Domain.Aggregates.UserAggregate;
using Domain.Aggregates.WorkAggregate;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;
using Test.Helpers;

namespace Application;

internal class SetupHelper
{
  private readonly ApplicationDbContext _dataAccess;

  public SetupHelper(ApplicationDbContext data) => _dataAccess = data;
  

  public static Mock<UserManager<IdentityUser>> GetMockUserManager()
  {
    Mock<IUserStore<IdentityUser>> userStoreMock = new();

    return new Mock<UserManager<IdentityUser>>(
      userStoreMock.Object,
      null,
      null,
      null,
      null,
      null,
      null,
      null,
      null
    );
  }

  public async Task<UserProfile> SetupUserAsync(string username, string password, IdentityUser? identityUser = null)
  {
    Mock<UserManager<IdentityUser>> userManager = GetMockUserManager();
    identityUser ??= new IdentityUser
    {
      Id = Guid.NewGuid().ToString(),
      UserName = username,
      PasswordHash = password
    };

    userManager.SetupSequence(x => x.FindByNameAsync(It.IsAny<string>())).ReturnsAsync((IdentityUser)null)
      .ReturnsAsync(identityUser);


    userManager.Setup(x =>
      x.CreateAsync(It.IsAny<IdentityUser>(), It.IsAny<string>())
    ).ReturnsAsync(IdentityResult.Success);

    userManager.Setup(x =>
      x.CheckPasswordAsync(It.IsAny<IdentityUser>(), It.IsAny<string>())
    ).ReturnsAsync(true);


    RegistrateUserHandler registrateUserHandler = new(_dataAccess, userManager.Object);
    RegistrateUserCommand request = new(username, password);

    return await registrateUserHandler.Handle(request, CancellationToken.None);
  }


  public async Task<WorkItem> SetupWorkItemAsync(string name)
  {
    Fixture fixture = new();
    string username = fixture.Create<string>();
    string password = fixture.Create<string>();


    var user = await SetupUserAsync(username, password);

    Mock<IMediator> mediatorMoq = new();
    mediatorMoq.Setup(x => x.Send(new GetUserByPublicIdQuery(user.PublicId),
        It.IsAny<CancellationToken>()
      )
    ).ReturnsAsync(user);

    CreateNewWorkItemHandler createNewWorkItemHandler = new(_dataAccess, mediatorMoq.Object);
    CreateNewWorkItemCommand request = new(name, user.PublicId);

    return await createNewWorkItemHandler.Handle(request, CancellationToken.None);
  }

  public async Task<WorkItem> SetupWorkItemAsync(string name, UserProfile user)
  {
    Mock<IMediator> mediatorMoq = new();
    mediatorMoq.Setup(x => x.Send(new GetUserByPublicIdQuery(user.PublicId),
        It.IsAny<CancellationToken>()
      )
    ).ReturnsAsync(user);

    CreateNewWorkItemHandler createNewWorkItemHandler = new(_dataAccess, mediatorMoq.Object);
    CreateNewWorkItemCommand request = new(name, user.PublicId);

    return await createNewWorkItemHandler.Handle(request, CancellationToken.None);
  }

  public async Task<WorkTime> SetupWorkTimeAsync(DateTimeOffset time, WorkItem workItem)
  {
    Mock<IMediator> mediatorMoq = new();
    mediatorMoq.Setup(x => x.Send(new GetWorkItemWithWorkTimeByPublicIdQuery(workItem.PublicId),
        It.IsAny<CancellationToken>()
      )
    ).ReturnsAsync(workItem);

    CreateWorkTimeHandler createWorkTimeHandler = new(_dataAccess, mediatorMoq.Object);
    CreateWorkTimeCommand request = new(time, workItem.PublicId);

    return await createWorkTimeHandler.Handle(request, CancellationToken.None);
  }
}

internal static class SetupHelperExtensien
{
  public static ApplicationDbContext CreateDataAccess<T>(this T caller)
  {
    DbContextOptions<ApplicationDbContext>? options = caller.CreatePostgreSqlUniqueClassOptions<ApplicationDbContext>();
    ApplicationDbContext dataAccess = new(options);
    dataAccess.Database.EnsureClean();
    return dataAccess;
  }
}