using Application.Common.Interfaces;
using Application.CQRS.Authentication.Commands;
using Application.CQRS.Authentication.Handlers;
using Application.CQRS.Authentication.Queries;
using Application.CQRS.WorkItems.Commands;
using Application.CQRS.WorkItems.Handlers;
using Application.CQRS.WorkItems.Queries;
using Application.CQRS.WorkTimes.Commands;
using Application.CQRS.WorkTimes.Handlers;
using Application.moq;
using Domain.Aggregates.UserAggregate;
using Domain.Aggregates.WorkAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Application;

internal class SetupHelper
{
  private readonly IApplicationDbContext _dataAccess;
  public SetupHelper(IApplicationDbContext data) => _dataAccess = data;

  public async static Task<IApplicationDbContext> CreateDataAccess()
  {
    DbContextOptions<ApplicationDbContextMoq>? options = SqliteInMemory.CreateOptions<ApplicationDbContextMoq>();
    ApplicationDbContextMoq dataAccessMoq = new(options);
    _ = await dataAccessMoq.Database.EnsureCreatedAsync();
    return dataAccessMoq;
  }


  public async Task<UserProfile> SetupUserAsync(string username, string password)
  {
    RegistrateUserHandler registrateUserHandler = new(_dataAccess);
    RegistrateUserCommand request = new(username, password);

    return await registrateUserHandler.Handle(request, CancellationToken.None);
  }

  public async Task<WorkItem> SetupWorkItemAsync(string name)
  {
    Fixture fixture = new();
    string username = fixture.Create<string>();
    string password = fixture.Create<string>();


    UserProfile user = await SetupUserAsync(username, password);

    Mock<IMediator>? mediatorMoq = new();
    _ = mediatorMoq.Setup(x => x.Send(new GetUserByPublicIdQuery(user.PublicId),
      It.IsAny<CancellationToken>())).ReturnsAsync(user);

    CreateNewWorkItemHandler createNewWorkItemHandler = new(_dataAccess, mediatorMoq.Object);
    CreateNewWorkItemCommand request = new(name, user.PublicId);

    return await createNewWorkItemHandler.Handle(request, CancellationToken.None);
  }

  public async Task<WorkItem> SetupWorkItemAsync(string name, UserProfile user)
  {
    Fixture fixture = new();

    Mock<IMediator>? mediatorMoq = new();
    _ = mediatorMoq.Setup(x => x.Send(new GetUserByPublicIdQuery(user.PublicId),
      It.IsAny<CancellationToken>())).ReturnsAsync(user);

    CreateNewWorkItemHandler createNewWorkItemHandler = new(_dataAccess, mediatorMoq.Object);
    CreateNewWorkItemCommand request = new(name, user.PublicId);

    return await createNewWorkItemHandler.Handle(request, CancellationToken.None);
  }

  public async Task<WorkTime> SetupWorkTimeAsync(DateTimeOffset time)
  {
    Fixture fixture = new();
    string name = fixture.Create<string>();

    WorkItem workItem = await SetupWorkItemAsync(name);
    Mock<IMediator>? mediatorMoq = new();
    _ = mediatorMoq.Setup(x => x.Send(new GetWorkItemWithWorkTimeByPublicIdQuery(workItem.PublicId),
      It.IsAny<CancellationToken>())).ReturnsAsync(workItem);

    CreateWorkTimeHandler createWorkTimeHandler = new(_dataAccess, mediatorMoq.Object);
    CreateWorkTimeCommand request = new(time, workItem.PublicId);

    return await createWorkTimeHandler.Handle(request, CancellationToken.None);
  }

  public async Task<WorkTime> SetupWorkTimeAsync(DateTimeOffset time, WorkItem workItem)
  {
    Fixture fixture = new();

    Mock<IMediator>? mediatorMoq = new();
    _ = mediatorMoq.Setup(x => x.Send(new GetWorkItemWithWorkTimeByPublicIdQuery(workItem.PublicId),
      It.IsAny<CancellationToken>())).ReturnsAsync(workItem);

    CreateWorkTimeHandler createWorkTimeHandler = new(_dataAccess, mediatorMoq.Object);
    CreateWorkTimeCommand request = new(time, workItem.PublicId);

    return await createWorkTimeHandler.Handle(request, CancellationToken.None);
  }
}