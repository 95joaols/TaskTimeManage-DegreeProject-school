﻿using Application.Common.Interfaces;
using Application.CQRS.Authentication.Queries;
using Application.CQRS.WorkItems.Commands;
using Application.CQRS.WorkItems.Handlers;

using Domain.Entities;

using MediatR;

using Moq;

namespace Application.Handlers.WorkItems;
public class CreateNewWorkItemHandlerTester
{
	[Fact]
	public async Task I_Can_Create_A_New_WorkItem()
	{
		//Arrange 
		Fixture fixture = new();
		string name = fixture.Create<string>();
		string username = fixture.Create<string>();
		string password = fixture.Create<string>();

		using IApplicationDbContext dataAccess = this.CreateDataAccess();


		SetupHelper helper = new(dataAccess);
		User user = await helper.SetupUserAsync(username, password);

		Mock<IMediator>? mediatorMoq = new();
		_ = mediatorMoq.Setup(x => x.Send(new GetUserByPublicIdQuery(user.PublicId),
		It.IsAny<CancellationToken>())).ReturnsAsync(user);

		CreateNewWorkItemHandler sut = new(dataAccess, mediatorMoq.Object);
		CreateNewWorkItemCommand request = new(name, user.PublicId);

		//Act
		WorkItem? results = await sut.Handle(request, CancellationToken.None);

		//Assert
		_ = results.Should().NotBeNull();
		_ = results.Id.Should().NotBe(0);
		_ = results.PublicId.Should().NotBeEmpty();
		_ = results.Name.Should().Be(name);
	}
}