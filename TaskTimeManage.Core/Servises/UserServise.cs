using Microsoft.EntityFrameworkCore;

using TaskTimeManage.Core.Security;
using TaskTimeManage.Domain.Context;
using TaskTimeManage.Domain.Entity;
using TaskTimeManage.Domain.Exceptions;

namespace TaskTimeManage.Core.Servises
{
    public class UserServise
    {
        private readonly TTMDbContext context;
        public UserServise(TTMDbContext context)
        {
            this.context = context;
        }
        public async Task<User?> GetUserByPublicIdAsync(Guid publicId)
        {
            return await context.User.FirstOrDefaultAsync(u => u.PublicId == publicId);

        }


        public async Task<User> CreateUserAsync(string username, string password)
        {
            User? user = await context.User.FirstOrDefaultAsync(u => u.UserName == username);
            if (user != null)
            {
                throw new UserAlreadyExistsException();
            }

            try
            {

                string salt = Cryptography.CreatSalt();
                string hashedPassword = Cryptography.Encrypt(Cryptography.Hash(Cryptography.Encrypt(password, salt), salt), salt);
                User createdUser = new(username, hashedPassword, salt);
                _ = await context.User.AddAsync(createdUser);
                int count = await context.SaveChangesAsync();
                return createdUser;
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        public async Task<string> Login(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentException($"'{nameof(username)}' cannot be null or whitespace.", nameof(username));
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException($"'{nameof(password)}' cannot be null or whitespace.", nameof(password));
            }

            User? user = await context.User.FirstOrDefaultAsync(u => u.UserName == username);
            if (user == null)
            {
                throw new LogInWrongException();
            }

            string hashedPassword = Cryptography.Encrypt(Cryptography.Hash(Cryptography.Encrypt(password, user.Salt), user.Salt), user.Salt);
            if (hashedPassword != user.Password)
            {
                throw new LogInWrongException();
            }

            return Token.GenerateToken(user);
        }
    }
}
