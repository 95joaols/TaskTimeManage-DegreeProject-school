namespace TaskTimeManage.Core.Exceptions;

public class UnableToDeleteWorkTimesException : Exception
{
	public UnableToDeleteWorkTimesException() : base($"Error Cant delete All Work Times")
	{
	}
}
