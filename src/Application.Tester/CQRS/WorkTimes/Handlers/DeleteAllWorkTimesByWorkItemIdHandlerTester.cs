using Application.Common.Interfaces;
using Application.CQRS.WorkTimes.Commands;

using Domain.Entities;

using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.WorkTimes.Handlers;
public class DeleteAllWorkTimesByWorkItemIdHandlerTester
{
  [Theory]
  [InlineData(1)]
  [InlineData(8)]
  [InlineData(4)]
  [InlineData(6)]
  [InlineData(7)]
  public async Task I_Can_Delete_All_WorkTime_For_WorkItem(int count)
  {
    //Arrange 
    Fixture fixture = new();
    fixture.Customizations.Add(new RandomDateTimeSequenceGenerator(DateTime.Now.AddYears(-2), DateTime.Now));

    string name = fixture.Create<string>();
    IEnumerable<DateTime> times = fixture.CreateMany<DateTime>(count);

    using IApplicationDbContext dataAccess = await SetupHelper.CreateDataAccess();

    SetupHelper helper = new(dataAccess);
    WorkItem workItem = await helper.SetupWorkItemAsync(name);
    foreach (DateTime time in times)
    {
      _ = await helper.SetupWorkTimeAsync(time.ToUniversalTime(), workItem);
    }

    DeleteAllWorkTimesByWorkItemIdHandler sut = new(dataAccess);
    DeleteAllWorkTimesByWorkItemIdCommand request = new(workItem.Id);

    //Act
    bool results = await sut.Handle(request, CancellationToken.None);

    //Assert
    _ = results.Should().BeTrue();
    _ = (await dataAccess.WorkTime.AnyAsync(x => x.WorkItemId == workItem.Id)).Should().BeFalse();
  }
}
