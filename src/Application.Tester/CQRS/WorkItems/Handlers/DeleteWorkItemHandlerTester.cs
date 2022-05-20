using Application.CQRS.WorkItems.Commands;
using Application.CQRS.WorkTimes.Commands;
using Application.moq;
using Domain.Aggregates.WorkAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Application.CQRS.WorkItems.Handlers;

public class DeleteWorkItemHandlerTester
{
  [Fact]
  public async Task I_Can_Delete_A_WorkItem()
  {
    //Arrange 
    Fixture fixture = new();
    string name = fixture.Create<string>();

    await using ApplicationDbContextMoq dataAccess = await SetupHelper.CreateDataAccess();

    SetupHelper helper = new(dataAccess);
    WorkItem workItem = await helper.SetupWorkItemAsync(name);

    Mock<IMediator>? mediatorMoq = new();
    _ = mediatorMoq.Setup(x => x.Send(new DeleteAllWorkTimesByWorkItemIdCommand(workItem.Id),
      It.IsAny<CancellationToken>())).ReturnsAsync(true);

    DeleteWorkItemHandler sut = new(dataAccess, mediatorMoq.Object);
    DeleteWorkItemCommand request = new(workItem.PublicId);

    //Act
    bool results = await sut.Handle(request, CancellationToken.None);

    //Assert
    _ = results.Should().BeTrue();
    _ = (await dataAccess.WorkItem.AnyAsync(x => x.Id == workItem.Id)).Should().BeFalse();
  }

  [Fact]
  public async Task It_Will_Stopp_If_It_Resive_False()
  {
    //Arrange 
    Fixture fixture = new();
    string name = fixture.Create<string>();

    await using ApplicationDbContextMoq dataAccess = await SetupHelper.CreateDataAccess();

    SetupHelper helper = new(dataAccess);
    WorkItem workItem = await helper.SetupWorkItemAsync(name);

    Mock<IMediator>? mediatorMoq = new();
    _ = mediatorMoq.Setup(x => x.Send(new DeleteAllWorkTimesByWorkItemIdCommand(workItem.Id),
      It.IsAny<CancellationToken>())).ReturnsAsync(false);

    DeleteWorkItemHandler sut = new(dataAccess, mediatorMoq.Object);
    DeleteWorkItemCommand request = new(workItem.PublicId);

    //Act
    //Assert
    _ = await sut.Invoking(s => s.Handle(request, CancellationToken.None)).Should().ThrowAsync<Exception>();
  }
}