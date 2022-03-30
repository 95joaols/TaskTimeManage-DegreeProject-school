namespace TaskTimeManage.Domain.DTO;
public class UserDTO
{
	public UserDTO(string name, string password)
	{
		Name = name;
		Password = password;
	}

	public string Name
	{
		get; set;
	}
	public string Password
	{
		get; set;
	}
}
