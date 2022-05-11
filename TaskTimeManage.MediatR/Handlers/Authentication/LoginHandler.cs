﻿using MediatR;

using Microsoft.EntityFrameworkCore;

using TaskTimeManage.MediatR.DataAccess;
using TaskTimeManage.MediatR.Exceptions;
using TaskTimeManage.MediatR.Models;
using TaskTimeManage.MediatR.Queries.Authentication;
using TaskTimeManage.MediatR.Security;

namespace TaskTimeManage.MediatR.Handlers.Authentication;
public class LoginHandler : IRequestHandler<LoginQuery, string>
{
	private readonly TTMDataAccess data;

	public LoginHandler(TTMDataAccess data) => this.data = data;

	public async Task<string> Handle(LoginQuery request, CancellationToken cancellationToken)
	{
		if (string.IsNullOrWhiteSpace(request.Username))
		{
			throw new ArgumentException($"'{nameof(request.Username)}' cannot be null or whitespace.", nameof(request.Username));
		}

		if (string.IsNullOrWhiteSpace(request.Password))
		{
			throw new ArgumentException($"'{nameof(request.Password)}' cannot be null or whitespace.", nameof(request.Password));
		}
		UserModel? user = await data.User.FirstOrDefaultAsync(u => u.UserName.ToLower() == request.Username.ToLower(), cancellationToken);

		if (user == null)
		{
			throw new LogInWrongException();
		}

		string hashedPassword = Cryptography.Encrypt(Cryptography.Hash(Cryptography.Encrypt(request.Password, user.Salt), user.Salt), user.Salt);
		if (hashedPassword != user.Password)
		{
			throw new LogInWrongException();
		}

		return Token.GenerateToken(user, request.TokenKey);
	}
}
