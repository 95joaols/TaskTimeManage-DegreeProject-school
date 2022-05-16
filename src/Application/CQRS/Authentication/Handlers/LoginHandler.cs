﻿using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Security;
using Application.CQRS.Authentication.Queries;

using Ardalis.GuardClauses;

using Domain.Entities;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Authentication.Handlers;
public class LoginHandler : IRequestHandler<LoginQuery, string>
{
	private readonly IApplicationDbContext data;

	public LoginHandler(IApplicationDbContext data) => this.data = data;

	public async Task<string> Handle(LoginQuery request, CancellationToken cancellationToken)
	{
		_ = Guard.Against.NullOrWhiteSpace(request.Username);
		_ = Guard.Against.NullOrWhiteSpace(request.Password);

		User? user = await data.User.FirstOrDefaultAsync(u => u.UserName.ToLower() == request.Username.Trim().ToLower(), cancellationToken);

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