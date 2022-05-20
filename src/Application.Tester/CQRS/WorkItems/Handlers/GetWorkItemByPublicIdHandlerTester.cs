using Application.CQRS.WorkItems.Queries;

namespace Application.CQRS.WorkItems.Handlers;

public class GetWorkItemByPublicIdHandlerTester
{
  [Fact]
  public async Task I_Can_Get_A_WorkItem_By_Its_PublicId()
  {
    //Arrange 
    Fixture fixture = new();
    string name = fixture.Create<string>();

    await using var dataAccess = await SetupHelper.CreateDataAccess();

    SetupHelper helper = new(dataAccess);
    var workItem = await helper.SetupWorkItemAsync(name);


    GetWorkItemByPublicIdHandler sut = new(dataAccess);
    GetWorkItemWithWorkTimeByPublicIdQuery request = new(workItem.PublicId);

    //Act
    var results = await sut.Handle(request, CancellationToken.None);

    //Assert
    results.Should().NotBeNull();
    results.Should().BeEquivalentTo(workItem);
  }
}