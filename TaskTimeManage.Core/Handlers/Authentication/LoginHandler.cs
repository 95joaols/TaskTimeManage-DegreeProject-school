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
		Guard(request);
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

	private static void Guard(LoginQuery request)
	{
		if (string.IsNullOrWhiteSpace(request.Username))
		{
			throw new ArgumentException($"'{nameof(request.Username)}' cannot be null or whitespace.", nameof(request.Username));
		}

		if (string.IsNullOrWhiteSpace(request.Password))
		{
			throw new ArgumentException($"'{nameof(request.Password)}' cannot be null or whitespace.", nameof(request.Password));
		}
	}
}
