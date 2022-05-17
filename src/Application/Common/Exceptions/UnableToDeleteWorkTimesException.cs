namespace Application.Common.Exceptions;

public class UnableToDeleteWorkTimesException : Exception
{
  public UnableToDeleteWorkTimesException() : base($"Error Cant delete All Work Times")
  {
  }
}
