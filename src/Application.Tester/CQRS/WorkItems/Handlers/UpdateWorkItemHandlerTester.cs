using Application.Common.Interfaces;
using Application.CQRS.WorkItems.Commands;
using Application.moq;

using Domain.Aggregates.WorkAggregate;

namespace Application.CQRS.WorkItems.Handlers;

public class UpdateWorkItemHandlerTester
{
  [Theory]
  [InlineData("fght6", "hxfhd5b")]
  [InlineData("jfjf6t", "xdhfgh5r h")]
  [InlineData("cfgj jyf", "dfhfbtrbh fh")]
  [InlineData("vbnr6hr7n", "fdhxfh f")]
  [InlineData("xghntntt67", "nht tr65")]
  [InlineData("tfdynd", "gfxhtryr6 t")]
  public async Task I_Can_Edit_The_Name_On_WorkItem(string oldName, string newName)
  {
    //Arrange 
    using ApplicationDbContextMoq dataAccess = await SetupHelper.CreateDataAccess();


    SetupHelper helper = new(dataAccess);
    WorkItem workItem = await helper.SetupWorkItemAsync(oldName);

    UpdateWorkItemHandler sut = new(dataAccess);
    UpdateWorkItemCommand request = new(workItem.PublicId, newName);

    //Act
    WorkItem? results = await sut.Handle(request, CancellationToken.None);

    //Assert
    _ = results.Should().NotBeNull();
    _ = results.Name.Should().Be(newName);
  }
}