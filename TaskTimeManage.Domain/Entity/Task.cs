namespace TaskTimeManage.Domain.Entity
{
    public class Task
    {
        public Task()
        {
        }
        public Task(string name, User user)
        {
            Name = name;
            User = user;
        }
        public int Id
        {
            get; set;
        }
        public string Name
        {
            get; set;
        }

        public int UserId
        {
            get; set;
        }
        public User User
        {
            get; set;
        }

    }
}
