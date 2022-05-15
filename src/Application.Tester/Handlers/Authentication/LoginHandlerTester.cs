﻿using Application.Common.Interfaces;
using Application.CQRS.Authentication.Handlers;
using Application.CQRS.Authentication.Queries;

using Domain.Entities;

using Microsoft.Extensions.Configuration;

namespace Application.Handlers.Authentication;
public class LoginHandlerTester
{
	[Fact]
	public async Task I_Can_Login_And_Get_A_Token()
	{
		//Arrange 
		using IApplicationDbContext dataAccess = this.CreateDataAccess();

		IConfigurationRoot? config = new ConfigurationBuilder()
		.SetBasePath(AppContext.BaseDirectory)
		.AddJsonFile("appsettings.json", false, true)
		.Build();

		Fixture fixture = new();
		string username = fixture.Create<string>();
		string password = fixture.Create<string>();
		string tokenKey = config.GetSection("AppSettings:Token").Value;

		SetupHelper helper = new(dataAccess);
		User user = await helper.SetupUserAsync(username, password);

		LoginHandler sut = new(dataAccess);
		LoginQuery request = new(username, password, tokenKey);

		//Act 
		string? results = await sut.Handle(request, CancellationToken.None);

		//Assert
		_ = results.Should().NotBeNullOrWhiteSpace();
	}
}
