using Application.CQRS.Authentication.Queries;
using Application.CQRS.WorkItems.Commands;
using MediatR;
using Moq;

namespace Application.CQRS.WorkItems.Handlers;

public class CreateNewWorkItemHandlerTester
{
  [Fact]
  public async Task I_Can_Create_A_New_WorkItem()
  {
    //Arrange 
    Fixture fixture = new();
    string name = fixture.Create<string>();
    string username = fixture.Create<string>();
    string password = fixture.Create<string>();

    await using var dataAccess = this.CreateDataAccess();


    SetupHelper helper = new(dataAccess);
    var user = await helper.SetupUserAsync(username, password);

    Mock<IMediator> mediatorMoq = new();
    mediatorMoq.Setup(x => x.Send(new GetUserByPublicIdQuery(user.PublicId),
        It.IsAny<CancellationToken>()
      )
    ).ReturnsAsync(user);

    CreateNewWorkItemHandler sut = new(dataAccess, mediatorMoq.Object);
    CreateNewWorkItemCommand request = new(name, user.PublicId);

    //Act
    var results = await sut.Handle(request, CancellationToken.None);

    //Assert
    results.Should().NotBeNull();
    results.Id.Should().NotBe(0);
    results.PublicId.Should().NotBeEmpty();
    results.Name.Should().Be(name);
  }
}