using Application.CQRS.WorkItems.Commands;
using Application.CQRS.WorkTimes.Commands;
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

    await using var dataAccess = this.CreateDataAccess();

    SetupHelper helper = new(dataAccess);
    var workItem = await helper.SetupWorkItemAsync(name);
    
    
    DeleteWorkItemHandler sut = new(dataAccess);
    DeleteWorkItemCommand request = new(workItem.PublicId);

    //Act
    bool results = await sut.Handle(request, CancellationToken.None);

    //Assert
    results.Should().BeTrue();
    (await dataAccess.WorkItem.AnyAsync(x => x.Id == workItem.Id)).Should().BeFalse();
  }
}