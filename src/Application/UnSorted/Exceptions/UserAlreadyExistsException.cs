namespace Application.Exceptions;

public class UserAlreadyExistsException : Exception
{
	public UserAlreadyExistsException() : base($"User Already Exists")
	{
	}
}
