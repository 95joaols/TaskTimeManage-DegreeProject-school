using Ardalis.GuardClauses;

using MediatR;

using Microsoft.EntityFrameworkCore;

using TaskTimeManage.Core.Commands.Authentication;
using TaskTimeManage.Core.DataAccess;
using TaskTimeManage.Core.Exceptions;
using TaskTimeManage.Core.Models;
using TaskTimeManage.Core.Security;

namespace TaskTimeManage.Core.Handlers.Authentication;
public class RegistrateUserHandler : IRequestHandler<RegistrateUserCommand, UserModel>
{
	private readonly TTMDataAccess data;

	public RegistrateUserHandler(TTMDataAccess data) => this.data = data;

	public async Task<UserModel> Handle(RegistrateUserCommand request, CancellationToken cancellationToken)
	{
		Guard.Against.NullOrWhiteSpace(request.Username);
		Guard.Against.NullOrWhiteSpace(request.Password);
		UserModel? user = await data.User.FirstOrDefaultAsync(u => u.UserName.ToLower() == request.Username.Trim().ToLower(), cancellationToken);
		if (user != null)
		{
			throw new UserAlreadyExistsException();
		}


		UserModel createdUser = CreateUser(request);
		_ = await data.User.AddAsync(createdUser, cancellationToken);
		_ = await data.SaveChangesAsync(cancellationToken);
		return createdUser;

	}

	private static UserModel CreateUser(RegistrateUserCommand request)
	{
		string salt = Cryptography.CreatSalt();
		string hashedPassword = Cryptography.Encrypt(Cryptography.Hash(Cryptography.Encrypt(request.Password.Trim(), salt), salt), salt);
		UserModel createdUser = new() {
			UserName = request.Username,
			Password = hashedPassword,
			Salt = salt,
		};
		return createdUser;
	}
}
