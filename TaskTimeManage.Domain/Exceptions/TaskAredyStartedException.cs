namespace TaskTimeManage.Domain.Exceptions
{
	public class TaskAredyStartedException : Exception
	{
		public TaskAredyStartedException() : base("Task Aredy Started. You need to end it first")
		{
		}
	}
}
