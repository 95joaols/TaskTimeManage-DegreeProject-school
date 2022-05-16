namespace Application.Common.Exceptions;

public class PasswordNotSameException : Exception
{
	public PasswordNotSameException() : base($"Password Not Same")
	{
	}
}
