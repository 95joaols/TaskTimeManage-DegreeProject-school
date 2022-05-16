using Application.Common.Interfaces;
using Application.CQRS.WorkItems.Queries;
using Application.CQRS.WorkTimes.Commands;

using Domain.Entities;

using MediatR;

using Moq;

namespace Application.CQRS.WorkTimes.Handlers;
public class CreateWorkTimeHandlerTester
{
	[Fact]
	public async Task I_Can_Create_A_New_WorkTime()
	{
		//Arrange 
		Fixture fixture = new();
		fixture.Customizations.Add(new RandomDateTimeSequenceGenerator(DateTime.Now.AddYears(-2), DateTime.Now));

		string name = fixture.Create<string>();
		DateTime time = fixture.Create<DateTime>().ToUniversalTime();


		using IApplicationDbContext dataAccess = this.CreateDataAccess();


		SetupHelper helper = new(dataAccess);
		WorkItem workItem = await helper.SetupWorkItemAsync(name);

		Mock<IMediator>? mediatorMoq = new();
		_ = mediatorMoq.Setup(x => x.Send(new GetWorkItemWithWorkTimeByPublicIdQuery(workItem.PublicId),
		It.IsAny<CancellationToken>())).ReturnsAsync(workItem);

		CreateWorkTimeHandler sut = new(dataAccess, mediatorMoq.Object);
		CreateWorkTimeCommand request = new(time, workItem.PublicId);

		//Act
		WorkTime? results = await sut.Handle(request, CancellationToken.None);

		//Assert
		_ = results.Should().NotBeNull();
		_ = results.Id.Should().NotBe(0);
		_ = results.PublicId.Should().NotBeEmpty();
		_ = results.Time.Should().Be(time);
	}
}
