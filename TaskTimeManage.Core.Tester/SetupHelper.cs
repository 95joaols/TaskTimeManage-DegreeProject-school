using AutoFixture;

using MediatR;

using Moq;

using TaskTimeManage.Core.Commands.Authentication;
using TaskTimeManage.Core.Commands.WorkItems;
using TaskTimeManage.Core.Handlers.Authentication;
using TaskTimeManage.Core.Handlers.WorkItems;
using TaskTimeManage.Core.Queries.Authentication;

namespace TaskTimeManage.Core;
internal class SetupHelper
{
	private readonly TTMDataAccess data;
	public SetupHelper(TTMDataAccess data)
	{
		this.data = data;
	}


	public async Task<UserModel> SetupUserAsync(string username, string password)
	{
		RegistrateUserHandler registrateUserHandler = new(data);
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

		CreateNewWorkItemHandler createNewWorkItemHandler = new(data, mediatorMoq.Object);
		CreateNewWorkItemCommand request = new(name, userModel.PublicId);

		return await createNewWorkItemHandler.Handle(request, CancellationToken.None);
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
