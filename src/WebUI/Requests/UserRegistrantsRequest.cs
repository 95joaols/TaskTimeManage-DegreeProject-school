namespace WebUI.Requests;

public class UserRegistrantsRequest
{
	public string Username
	{
		get; set;
	}
	public string Password
	{
		get; set;
	}
	public string RepeatPassword
	{
		get; set;
	}
}
