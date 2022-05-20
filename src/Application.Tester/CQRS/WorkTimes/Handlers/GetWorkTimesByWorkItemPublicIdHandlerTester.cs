using Application.CQRS.WorkTimes.Queries;
using Application.moq;
using Domain.Aggregates.WorkAggregate;

namespace Application.CQRS.WorkTimes.Handlers;

public class GetWorkTimesByWorkItemPublicIdHandlerTester
{
  [Theory]
  [InlineData(1)]
  [InlineData(8)]
  [InlineData(4)]
  [InlineData(6)]
  [InlineData(7)]
  public async Task I_Can_Get_All_WorkTime_For_WorkItem(int count)
  {
    //Arrange 
    Fixture fixture = new();
    fixture.Customizations.Add(new RandomDateTimeSequenceGenerator(DateTimeOffset.Now.AddYears(-2).DateTime,
        DateTimeOffset.Now.DateTime
      )
    );
    string name = fixture.Create<string>();
    IEnumerable<DateTime> times = fixture.CreateMany<DateTime>(count);

    await using ApplicationDbContextMoq dataAccess = await SetupHelper.CreateDataAccess();

    SetupHelper helper = new(dataAccess);
    WorkItem workItem = await helper.SetupWorkItemAsync(name);
    List<WorkTime> workTimes = new();

    foreach (DateTime time in times)
    {
      workTimes.Add(await helper.SetupWorkTimeAsync(time, workItem));
    }


    GetWorkTimesByWorkItemPublicIdHandler sut = new(dataAccess);
    GetWorkTimesByWorkItemPublicIdQuery request = new(workItem.PublicId);

    //Act
    IEnumerable<WorkTime>? results = await sut.Handle(request, CancellationToken.None);

    //Assert
    _ = results.Should().NotBeNullOrEmpty();
    _ = results.Should().HaveCount(count);
    _ = results.ToList().Should().BeEquivalentTo(workTimes, options =>
      options.ExcludingNestedObjects()
    );
  }
}