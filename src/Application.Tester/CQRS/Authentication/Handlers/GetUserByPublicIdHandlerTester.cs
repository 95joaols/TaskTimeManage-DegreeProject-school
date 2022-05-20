using Application.CQRS.Authentication.Queries;

namespace Application.CQRS.Authentication.Handlers;

public class GetUserByPublicIdHandlerTester
{
  [Fact]
  public async Task I_Can_Get_A_User()
  {
    //Arrange 
    await using var dataAccess = await SetupHelper.CreateDataAccess();

    SetupHelper helper = new(dataAccess);
    var user = await helper.SetupUserAsync("Test", "Test");

    GetUserByPublicIdHandler sut = new(dataAccess);
    GetUserByPublicIdQuery request = new(user.PublicId);

    //Act 
    var results = await sut.Handle(request, CancellationToken.None);

    //Assert
    results.Should().NotBeNull();
    results.Should().BeEquivalentTo(user);
  }
}