using Microsoft.EntityFrameworkCore;

using TaskTimeManage.Core.Security;
using TaskTimeManage.Domain.Context;
using TaskTimeManage.Domain.Entity;
using TaskTimeManage.Domain.Exceptions;

namespace TaskTimeManage.Core.Service;

public class UserService
{
	private readonly TTMDbContext context;
	public UserService(TTMDbContext context) => this.context = context;
	public async Task<User?> GetUserByPublicIdAsync(Guid publicId) => await context.User.FirstOrDefaultAsync(u => u.PublicId == publicId);


	public async Task<User> CreateUserAsync(string username, string password, CancellationToken cancellationToken)
	{
		User? user = await context.User.FirstOrDefaultAsync(u => u.UserName == username, cancellationToken);
		if (user != null)
		{
			throw new UserAlreadyExistsException();
		}

		try
		{

			string salt = Cryptography.CreatSalt();
			string hashedPassword = Cryptography.Encrypt(Cryptography.Hash(Cryptography.Encrypt(password, salt), salt), salt);
			User createdUser = new(username, hashedPassword, salt);
			_ = await context.User.AddAsync(createdUser, cancellationToken);
			int count = await context.SaveChangesAsync(cancellationToken);
			return createdUser;
		}
		catch (Exception)
		{
			throw new Exception();
		}
	}

	public async Task<string> LoginAsync(string username, string password,string tokenKey, CancellationToken cancellationToken)
	{
		if (string.IsNullOrWhiteSpace(username))
		{
			throw new ArgumentException($"'{nameof(username)}' cannot be null or whitespace.", nameof(username));
		}

		if (string.IsNullOrWhiteSpace(password))
		{
			throw new ArgumentException($"'{nameof(password)}' cannot be null or whitespace.", nameof(password));
		}

		User? user = await context.User.FirstOrDefaultAsync(u => u.UserName.ToLower() == username.ToLower(), cancellationToken);
		if (user == null)
		{
			throw new LogInWrongException();
		}

		string hashedPassword = Cryptography.Encrypt(Cryptography.Hash(Cryptography.Encrypt(password, user.Salt), user.Salt), user.Salt);
		if (hashedPassword != user.Password)
		{
			throw new LogInWrongException();
		}

		return Token.GenerateToken(user,tokenKey);
	}
}
