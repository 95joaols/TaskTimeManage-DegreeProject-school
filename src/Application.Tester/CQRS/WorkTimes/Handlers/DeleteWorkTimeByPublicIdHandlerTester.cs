using Application.CQRS.WorkTimes.Commands;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.WorkTimes.Handlers;

public class DeleteWorkTimeByPublicIdHandlerTester
{
  [Fact]
  public async Task I_Can_Delete_WorkTime_By_Its_PublicId()
  {
    //Arrange 
    Fixture fixture = new();
    fixture.Customizations.Add(new RandomDateTimeSequenceGenerator(DateTimeOffset.Now.AddYears(-2).DateTime,
        DateTimeOffset.Now.DateTime
      )
    );
    fixture.Create<string>();
    var time = fixture.Create<DateTime>();

    await using var dataAccess = await SetupHelper.CreateDataAccess();

    SetupHelper helper = new(dataAccess);
    var workTime = await helper.SetupWorkTimeAsync(time);


    DeleteWorkTimeByPublicIdHandler sut = new(dataAccess);
    DeleteWorkTimeByPublicIdCommand request = new(workTime.PublicId);

    //Act
    bool results = await sut.Handle(request, CancellationToken.None);

    //Assert
    results.Should().BeTrue();
    (await dataAccess.WorkTime.AnyAsync(x => x.Id == workTime.Id)).Should().BeFalse();
  }
}