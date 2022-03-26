using Microsoft.EntityFrameworkCore;

using TaskTimeManage.Domain.Context;
using TaskTimeManage.Domain.Entity;
using TaskTimeManage.Domain.Exceptions;
using TaskTimeManage.Domain.Security;

namespace TaskTimeManage.Core.Servises
{
    public class UserServise
    {
        private readonly TTMDbContext context;
        public UserServise(TTMDbContext context)
        {
            this.context = context;
        }
        public async Task<User?> GetUserByNameAsync(string username)
        {
            return await context.User.FirstOrDefaultAsync(u => u.UserName == username);

        }


        public async Task<bool> CreateUserAsync(string username, string password)
        {
            User? user = await context.User.FirstOrDefaultAsync(u => u.UserName == username);
            if (user != null)
                throw new UserAlreadyExists();
            try
            {

                string salt = Cryptography.CreatSalt();
                string hashedPassword = Cryptography.Encrypt(Cryptography.Hash(Cryptography.Encrypt(password, salt), salt), salt);
                await context.User.AddAsync(new User(username, hashedPassword, salt));
                int count = await context.SaveChangesAsync();
                return count > 0;
            }
            catch (Exception)
            {
                return false;
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
                throw new LogInWrongException();

            string hashedPassword = Cryptography.Encrypt(Cryptography.Hash(Cryptography.Encrypt(password, user.Salt), user.Salt), user.Salt);
            if (hashedPassword != user.Password)
                throw new LogInWrongException();

            return Token.GenerateToken(user);
        }
    }
}
