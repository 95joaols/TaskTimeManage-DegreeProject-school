using Application.Common.Interfaces;
using Application.Common.Models.Generated;
using Application.CQRS.WorkTimes.Commands;

using Domain.Entities;

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
    fixture.Customizations.Add(new RandomDateTimeSequenceGenerator(DateTime.Now.AddYears(-2), DateTime.Now));

    string name = fixture.Create<string>();
    IEnumerable<DateTime> times = fixture.CreateMany<DateTime>(count);

    using IApplicationDbContext dataAccess = await SetupHelper.CreateDataAccess();

    SetupHelper helper = new(dataAccess);
    WorkItem workItem = await helper.SetupWorkItemAsync(name);
    List<WorkTime> WorkTimes = new();

    foreach (DateTime time in times)
    {
      WorkTimes.Add(await helper.SetupWorkTimeAsync(time.ToUniversalTime(), workItem));
    }
    List<WorkTimeDto> toUpdate = new();
    foreach (WorkTime? WorkTime in WorkTimes)
    {
      toUpdate.Add(new() {
        PublicId = WorkTime.PublicId,
        Time = fixture.Create<DateTime>().ToUniversalTime()

      });
    }


    UpdateWorkTimesHandler sut = new(dataAccess);
    UpdateWorkTimesCommand request = new(toUpdate);

    //Act
    IEnumerable<WorkTime>? results = await sut.Handle(request, CancellationToken.None);

    //Assert
    _ = results.Should().NotBeNullOrEmpty();
    _ = results.Should().HaveCount(count);
    foreach (WorkTime? result in results)
    {
      _ = result.Time.Should().Be(toUpdate.FirstOrDefault(x => x.PublicId == result.PublicId).Time);
    }
  }
}
