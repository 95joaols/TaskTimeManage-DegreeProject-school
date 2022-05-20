namespace Application.Common.Exceptions;

public class LogInWrongException : Exception
{
  public LogInWrongException() : base("Username or Password is wrong")
  {
  }
}