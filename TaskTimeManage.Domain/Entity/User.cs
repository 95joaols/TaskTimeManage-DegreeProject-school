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

        public User(int id, string userName, string password, DateTime creationTime, string salt)
        {
            Id = id;
            UserName = userName;
            Password = password;
            CreationTime = creationTime;
            Salt = salt;
        }

        public int Id
        {
            get; set;
        }
        public string UserName
        {
            get; set;
        }
        public string Password
        {
            get; set;
        }
        public string Salt
        {
            get; set;
        }
        public DateTime CreationTime
        {
            get; set;
        }
    }
}
