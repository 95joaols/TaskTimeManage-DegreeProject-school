﻿using Ardalis.GuardClauses;

using MediatR;

using Microsoft.EntityFrameworkCore;

using TaskTimeManage.Core.DataAccess;
using TaskTimeManage.Core.Exceptions;
using TaskTimeManage.Core.Models;
using TaskTimeManage.Core.Queries.Authentication;
using TaskTimeManage.Core.Security;

namespace TaskTimeManage.Core.Handlers.Authentication;
public class LoginHandler : IRequestHandler<LoginQuery, string>
{
	private readonly TTMDataAccess data;

	public LoginHandler(TTMDataAccess data) => this.data = data;

	public async Task<string> Handle(LoginQuery request, CancellationToken cancellationToken)
	{
		Guard.Against.NullOrWhiteSpace(request.Username);
		Guard.Against.NullOrWhiteSpace(request.Password);

		UserModel? user = await data.User.FirstOrDefaultAsync(u => u.UserName.ToLower() == request.Username.Trim().ToLower(), cancellationToken);

		if (user == null)
		{
			throw new LogInWrongException();
		}

		string hashedPassword = Cryptography.Encrypt(Cryptography.Hash(Cryptography.Encrypt(request.Password.Trim(), user.Salt), user.Salt), user.Salt);
		if (hashedPassword != user.Password)
		{
			throw new LogInWrongException();
		}

		return Token.GenerateToken(user, request.TokenKey);
	}
}