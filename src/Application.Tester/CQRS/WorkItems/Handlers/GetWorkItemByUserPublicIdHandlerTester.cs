﻿using Application.CQRS.WorkItems.Queries;
using Domain.Aggregates.WorkAggregate;

namespace Application.CQRS.WorkItems.Handlers;

public class GetWorkItemByUserPublicIdHandlerTester
{
  [Theory]
  [InlineData(1)]
  [InlineData(8)]
  [InlineData(4)]
  [InlineData(6)]
  [InlineData(7)]
  public async Task I_Can_Get_All_WorkItem_For_A_User(int count)
  {
    //Arrange 
    Fixture fixture = new();
    IEnumerable<string> names = fixture.CreateMany<string>(count);
    string username = fixture.Create<string>();
    string password = fixture.Create<string>();
    await using var dataAccess = this.CreateDataAccess();
    List<WorkItem> workItems = new();


    SetupHelper helper = new(dataAccess);
    var user = await helper.SetupUserAsync(username, password);
    foreach (string? name in names)
    {
      workItems.Add(await helper.SetupWorkItemAsync(name, user));
    }

    GetWorkItemByUserPublicIdHandler sut = new(dataAccess);
    GetWorkItemTimeByUserPublicIdQuery request = new(user.PublicId);

    //Act
    IEnumerable<WorkItem> results = await sut.Handle(request, CancellationToken.None);

    //Assert
    results.Should().NotBeNullOrEmpty();
    results.Should().HaveCount(count);
    results.ToList().Should().BeEquivalentTo(workItems,
      options =>
        options.ExcludingNestedObjects()
    );
  }
}