using MediatR;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TaskTimeManage.MediatR.Commands.Authentication;
using TaskTimeManage.MediatR.DataAccess;
using TaskTimeManage.MediatR.Exceptions;
using TaskTimeManage.MediatR.Models;
using TaskTimeManage.MediatR.Security;

namespace TaskTimeManage.MediatR.Handlers.Authentication;
public class RegistrateUserHandler : IRequestHandler<RegistrateUserCommand, UserModel>
{
	private readonly TTMDataAccess data;

	public RegistrateUserHandler(TTMDataAccess data) => this.data = data;

	public async Task<UserModel> Handle(RegistrateUserCommand request, CancellationToken cancellationToken)
	{
		UserModel? user = await data.User.FirstOrDefaultAsync(u => u.UserName == request.Username, cancellationToken);
		if (user != null)
		{
			throw new UserAlreadyExistsException();
		}

		try
		{

			string salt = Cryptography.CreatSalt();
			string hashedPassword = Cryptography.Encrypt(Cryptography.Hash(Cryptography.Encrypt(request.Password, salt), salt), salt);
			UserModel createdUser = new() {
				UserName = request.Username,
				Password = hashedPassword,
				Salt = salt,
			};
			_ = await data.User.AddAsync(createdUser, cancellationToken);
			await data.SaveChangesAsync(cancellationToken);
			return createdUser;
		}
		catch (Exception)
		{
			throw new Exception();
		}
	}
}
