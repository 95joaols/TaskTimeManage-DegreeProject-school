namespace TaskTimeManage.Domain.Exceptions
{
    public class UserAlreadyExists : Exception
    {
        public UserAlreadyExists() : base($"User Already Exists")
        {
        }
    }
}
