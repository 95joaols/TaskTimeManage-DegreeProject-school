﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskTimeManage.Domain.Entity;

public class User
{
	public User(string userName, string password, string salt)
	{
		UserName = userName;
		Password = password;
		Salt = salt;
	}

	public User()
	{
	}
	[Key]
	public int Id
	{
		get; set;
	}

	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public Guid PublicId
	{
		get; set;
	}
	[Required]
	public string UserName
	{
		get; set;
	}
	[Required]
	public string Password
	{
		get; set;
	}
	[Required]
	public string Salt
	{
		get; set;
	}
	public List<WorkItem> Tasks
	{
		get; set;
	} = new();

	public DateTime CreationTime
	{
		get; set;
	}
}
