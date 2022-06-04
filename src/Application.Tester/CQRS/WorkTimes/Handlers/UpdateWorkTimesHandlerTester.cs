using Application.CQRS.WorkItems.Queries;
using Application.CQRS.WorkTimes.Commands;
using Domain.Aggregates.WorkAggregate;
using MediatR;
using Moq;

namespace Application.CQRS.WorkTimes.Handlers;

public class UpdateWorkTimesHandlerTester
{
  [Theory]
  [InlineData(1)]
  [InlineData(8)]
  [InlineData(4)]
  [InlineData(6)]
  [InlineData(7)]
  public async Task I_Can_Update_List_Of_WorkTime(int count)
  {
    //Arrange 
    Fixture fixture = new();
    fixture.Customizations.Add(new RandomDateTimeSequenceGenerator(DateTimeOffset.Now.AddYears(-2).DateTime,
        DateTimeOffset.Now.DateTime
      )
    );

    string name = fixture.Create<string>();
    IEnumerable<DateTimeOffset> times = fixture.CreateMany<DateTimeOffset>(count);

    await using var dataAccess = this.CreateDataAccess();

    SetupHelper helper = new(dataAccess);
    var workItem = await helper.SetupWorkItemAsync(name);
    List<WorkTime> workTimes = new();

    foreach (var time in times)
    {
      workTimes.Add(await helper.SetupWorkTimeAsync(time, workItem));
    }

    List<WorkTime> toUpdate = workTimes.Select(workTime => WorkTime.CreateWorkTime(workTime.PublicId, fixture.Create<DateTime>())).ToList();

    Mock<IMediator> mediatorMoq = new();
    mediatorMoq.Setup(x => x.Send(new GetWorkItemWithWorkTimeByPublicIdQuery(workItem.PublicId),
        It.IsAny<CancellationToken>()
      )
    ).ReturnsAsync(workItem);


    UpdateWorkTimesHandler sut = new(dataAccess,mediatorMoq.Object);
    UpdateWorkTimesCommand request = new(workItem.PublicId,toUpdate);

    //Act
    IEnumerable<WorkTime> results = await sut.Handle(request, CancellationToken.None);

    //Assert
    results.Should().NotBeNullOrEmpty();
    results.Should().HaveCount(count);
    foreach (var result in results)
    {
      result.Time.Should().Be(toUpdate.FirstOrDefault(x => x.PublicId == result.PublicId).Time);
    }
  }
}