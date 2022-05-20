using Application.CQRS.WorkTimes.Queries;
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

    await using var dataAccess = await SetupHelper.CreateDataAccess();

    SetupHelper helper = new(dataAccess);
    var workItem = await helper.SetupWorkItemAsync(name);
    List<WorkTime> workTimes = new();

    foreach (var time in times)
    {
      workTimes.Add(await helper.SetupWorkTimeAsync(time, workItem));
    }


    GetWorkTimesByWorkItemPublicIdHandler sut = new(dataAccess);
    GetWorkTimesByWorkItemPublicIdQuery request = new(workItem.PublicId);

    //Act
    IEnumerable<WorkTime> results = await sut.Handle(request, CancellationToken.None);

    //Assert
    results.Should().NotBeNullOrEmpty();
    results.Should().HaveCount(count);
    results.ToList().Should().BeEquivalentTo(workTimes,
      options =>
        options.ExcludingNestedObjects()
    );
  }
}