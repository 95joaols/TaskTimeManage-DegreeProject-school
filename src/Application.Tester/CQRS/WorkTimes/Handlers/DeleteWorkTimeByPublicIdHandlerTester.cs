using Application.CQRS.WorkItems.Queries;
using Application.CQRS.WorkTimes.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;

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
    var name = fixture.Create<string>();

    await using var dataAccess = this.CreateDataAccess();

    SetupHelper helper = new(dataAccess);
    var workItem = await helper.SetupWorkItemAsync(name);
    var workTime = await helper.SetupWorkTimeAsync(time,workItem);

    Mock<IMediator> mediatorMoq = new();
    mediatorMoq.Setup(x => x.Send(new GetWorkItemWithWorkTimeByPublicIdQuery(workItem.PublicId),
        It.IsAny<CancellationToken>()
      )
    ).ReturnsAsync(workItem);

    DeleteWorkTimeByPublicIdHandler sut = new(dataAccess,mediatorMoq.Object);
    DeleteWorkTimeByPublicIdCommand request = new(workItem.PublicId, workTime.PublicId);

    //Act
    bool results = await sut.Handle(request, CancellationToken.None);

    //Assert
    results.Should().BeTrue();
  }
}