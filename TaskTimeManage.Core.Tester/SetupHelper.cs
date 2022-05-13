using MediatR;

using Moq;

using TaskTimeManage.Core.Commands.Authentication;
using TaskTimeManage.Core.Commands.WorkItems;
using TaskTimeManage.Core.Commands.WorkTimes;
using TaskTimeManage.Core.Handlers.Authentication;
using TaskTimeManage.Core.Handlers.WorkItems;
using TaskTimeManage.Core.Handlers.WorkTimes;
using TaskTimeManage.Core.Queries.Authentication;
using TaskTimeManage.Core.Queries.WorkItems;

namespace TaskTimeManage.Core;
internal class SetupHelper
{
	private readonly TTMDataAccess dataAccess;
	public SetupHelper(TTMDataAccess data)
	{
		this.dataAccess = data;
	}


	public async Task<UserModel> SetupUserAsync(string username, string password)
	{
		RegistrateUserHandler registrateUserHandler = new(dataAccess);
		RegistrateUserCommand request = new(username, password);

		return await registrateUserHandler.Handle(request, CancellationToken.None);
	}

	public async Task<WorkItemModel> SetupWorkItemAsync(string name)
	{
		Fixture fixture = new();
		string username = fixture.Create<string>();
		string password = fixture.Create<string>();


		UserModel userModel = await SetupUserAsync(username, password);

		Mock<IMediator>? mediatorMoq = new Mock<IMediator>();
		mediatorMoq.Setup(x => x.Send(new GetUserByPublicIdQuery(userModel.PublicId),
		It.IsAny<CancellationToken>())).ReturnsAsync(userModel);

		CreateNewWorkItemHandler createNewWorkItemHandler = new(dataAccess, mediatorMoq.Object);
		CreateNewWorkItemCommand request = new(name, userModel.PublicId);

		return await createNewWorkItemHandler.Handle(request, CancellationToken.None);
	}
	public async Task<WorkItemModel> SetupWorkItemAsync(string name, UserModel userModel)
	{
		Fixture fixture = new();
		string username = fixture.Create<string>();
		string password = fixture.Create<string>();


		Mock<IMediator>? mediatorMoq = new Mock<IMediator>();
		mediatorMoq.Setup(x => x.Send(new GetUserByPublicIdQuery(userModel.PublicId),
		It.IsAny<CancellationToken>())).ReturnsAsync(userModel);

		CreateNewWorkItemHandler createNewWorkItemHandler = new(dataAccess, mediatorMoq.Object);
		CreateNewWorkItemCommand request = new(name, userModel.PublicId);

		return await createNewWorkItemHandler.Handle(request, CancellationToken.None);
	}
	public async Task<WorkTimeModel> SetupWorkTimeAsync(DateTime time)
	{
		Fixture fixture = new();
		string name = fixture.Create<string>();

		WorkItemModel workItemModel = await SetupWorkItemAsync(name);
		Mock<IMediator>? mediatorMoq = new Mock<IMediator>();
		mediatorMoq.Setup(x => x.Send(new GetWorkItemWithWorkTimeByPublicIdQuery(workItemModel.PublicId),
		It.IsAny<CancellationToken>())).ReturnsAsync(workItemModel);

		CreateWorkTimeHandler createWorkTimeHandler = new(dataAccess, mediatorMoq.Object);
		CreateWorkTimeCommand request = new(time, workItemModel.PublicId);

		return await createWorkTimeHandler.Handle(request, CancellationToken.None);
	}
	public async Task<WorkTimeModel> SetupWorkTimeAsync(DateTime time, WorkItemModel workItemModel)
	{
		Fixture fixture = new();
		string name = fixture.Create<string>();

		Mock<IMediator>? mediatorMoq = new Mock<IMediator>();
		mediatorMoq.Setup(x => x.Send(new GetWorkItemWithWorkTimeByPublicIdQuery(workItemModel.PublicId),
		It.IsAny<CancellationToken>())).ReturnsAsync(workItemModel);

		CreateWorkTimeHandler createWorkTimeHandler = new(dataAccess, mediatorMoq.Object);
		CreateWorkTimeCommand request = new(time, workItemModel.PublicId);

		return await createWorkTimeHandler.Handle(request, CancellationToken.None);
	}

}
internal static class SetupHelperExtensien
{
	public static TTMDataAccess CreateDataAccess<T>(this T caller)
	{
		Microsoft.EntityFrameworkCore.DbContextOptions<TTMDataAccess>? options = caller.CreatePostgreSqlUniqueClassOptions<TTMDataAccess>();
		TTMDataAccess dataAccess = new TTMDataAccess(options);
		dataAccess.Database.EnsureClean();
		return dataAccess;
	}
}
