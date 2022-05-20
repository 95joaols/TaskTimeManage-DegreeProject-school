using Application.CQRS.WorkItems.Queries;
using Application.CQRS.WorkTimes.Commands;
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
    fixture.Customizations.Add(new RandomDateTimeSequenceGenerator(DateTimeOffset.Now.AddYears(-2).DateTime,
        DateTimeOffset.Now.DateTime
      )
    );

    string name = fixture.Create<string>();
    var time = fixture.Create<DateTimeOffset>();


    await using var dataAccess = await SetupHelper.CreateDataAccess();


    SetupHelper helper = new(dataAccess);
    var workItem = await helper.SetupWorkItemAsync(name);

    Mock<IMediator> mediatorMoq = new();
    mediatorMoq.Setup(x => x.Send(new GetWorkItemWithWorkTimeByPublicIdQuery(workItem.PublicId),
        It.IsAny<CancellationToken>()
      )
    ).ReturnsAsync(workItem);

    CreateWorkTimeHandler sut = new(dataAccess, mediatorMoq.Object);
    CreateWorkTimeCommand request = new(time, workItem.PublicId);

    //Act
    var results = await sut.Handle(request, CancellationToken.None);

    //Assert
    results.Should().NotBeNull();
    results.Id.Should().NotBe(0);
    results.PublicId.Should().NotBeEmpty();
    results.Time.Should().Be(time);
  }
}