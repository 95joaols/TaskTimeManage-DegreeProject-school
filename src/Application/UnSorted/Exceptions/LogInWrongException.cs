namespace Application.Exceptions;

public class LogInWrongException : Exception
{
	public LogInWrongException() : base($"Username or Password is wrong")
	{
	}
}
