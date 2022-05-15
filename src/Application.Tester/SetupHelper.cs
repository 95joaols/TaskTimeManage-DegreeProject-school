using Application.Common.Interfaces;
using Application.CQRS.Authentication.Commands;
using Application.CQRS.Authentication.Handlers;
using Application.CQRS.Authentication.Queries;
using Application.CQRS.WorkItems.Commands;
using Application.CQRS.WorkItems.Handlers;
using Application.CQRS.WorkItems.Queries;
using Application.CQRS.WorkTimes.Commands;
using Application.CQRS.WorkTimes.Handlers;

using Domain.Entities;

using Infrastructure.Persistence;

using MediatR;

using Microsoft.EntityFrameworkCore;

using Moq;

namespace Application;
internal class SetupHelper
{
	private readonly IApplicationDbContext dataAccess;
	public SetupHelper(IApplicationDbContext data) => dataAccess = data;


	public async Task<User> SetupUserAsync(string username, string password)
	{
		RegistrateUserHandler registrateUserHandler = new(dataAccess);
		RegistrateUserCommand request = new(username, password);

		return await registrateUserHandler.Handle(request, CancellationToken.None);
	}

	public async Task<WorkItem> SetupWorkItemAsync(string name)
	{
		Fixture fixture = new();
		string username = fixture.Create<string>();
		string password = fixture.Create<string>();


		User User = await SetupUserAsync(username, password);

		Mock<IMediator>? mediatorMoq = new();
		_ = mediatorMoq.Setup(x => x.Send(new GetUserByPublicIdQuery(User.PublicId),
		It.IsAny<CancellationToken>())).ReturnsAsync(User);

		CreateNewWorkItemHandler createNewWorkItemHandler = new(dataAccess, mediatorMoq.Object);
		CreateNewWorkItemCommand request = new(name, User.PublicId);

		return await createNewWorkItemHandler.Handle(request, CancellationToken.None);
	}
	public async Task<WorkItem> SetupWorkItemAsync(string name, User User)
	{
		Fixture fixture = new();
		string username = fixture.Create<string>();
		string password = fixture.Create<string>();


		Mock<IMediator>? mediatorMoq = new();
		_ = mediatorMoq.Setup(x => x.Send(new GetUserByPublicIdQuery(User.PublicId),
		It.IsAny<CancellationToken>())).ReturnsAsync(User);

		CreateNewWorkItemHandler createNewWorkItemHandler = new(dataAccess, mediatorMoq.Object);
		CreateNewWorkItemCommand request = new(name, User.PublicId);

		return await createNewWorkItemHandler.Handle(request, CancellationToken.None);
	}
	public async Task<WorkTime> SetupWorkTimeAsync(DateTime time)
	{
		Fixture fixture = new();
		string name = fixture.Create<string>();

		WorkItem WorkItem = await SetupWorkItemAsync(name);
		Mock<IMediator>? mediatorMoq = new();
		_ = mediatorMoq.Setup(x => x.Send(new GetWorkItemWithWorkTimeByPublicIdQuery(WorkItem.PublicId),
		It.IsAny<CancellationToken>())).ReturnsAsync(WorkItem);

		CreateWorkTimeHandler createWorkTimeHandler = new(dataAccess, mediatorMoq.Object);
		CreateWorkTimeCommand request = new(time, WorkItem.PublicId);

		return await createWorkTimeHandler.Handle(request, CancellationToken.None);
	}
	public async Task<WorkTime> SetupWorkTimeAsync(DateTime time, WorkItem WorkItem)
	{
		Fixture fixture = new();
		string name = fixture.Create<string>();

		Mock<IMediator>? mediatorMoq = new();
		_ = mediatorMoq.Setup(x => x.Send(new GetWorkItemWithWorkTimeByPublicIdQuery(WorkItem.PublicId),
		It.IsAny<CancellationToken>())).ReturnsAsync(WorkItem);

		CreateWorkTimeHandler createWorkTimeHandler = new(dataAccess, mediatorMoq.Object);
		CreateWorkTimeCommand request = new(time, WorkItem.PublicId);

		return await createWorkTimeHandler.Handle(request, CancellationToken.None);
	}

}
static internal class SetupHelperExtensien
{
	public static IApplicationDbContext CreateDataAccess<T>(this T caller)
	{
		DbContextOptions<ApplicationDbContext>? options = caller.CreatePostgreSqlUniqueClassOptions<ApplicationDbContext>();
		ApplicationDbContext dataAccess = new(options);
		dataAccess.Database.EnsureClean();
		return dataAccess;
	}
}
