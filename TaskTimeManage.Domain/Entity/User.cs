using System.ComponentModel.DataAnnotations;

namespace TaskTimeManage.Domain.Entity
{
    public class User
    {
        public User(string userName, string password, string salt)
        {
            UserName = userName;
            Password = password;
            Salt = salt;
        }

        public User()
        {
        }
        [Key]
        public int Id
        {
            get; set;
        }
        [Required]
        public string UserName
        {
            get; set;
        }
        [Required]
        public string Password
        {
            get; set;
        }
        [Required]
        public string Salt
        {
            get; set;
        }
        public List<Task> Tasks
        {
            get; set;
        } = new();

        public DateTime CreationTime
        {
            get; set;
        }
    }
}
