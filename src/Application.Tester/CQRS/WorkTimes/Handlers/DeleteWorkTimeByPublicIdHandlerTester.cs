using Application.Common.Interfaces;
using Application.CQRS.WorkTimes.Commands;

using Domain.Entities;

using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.WorkTimes.Handlers;
public class DeleteWorkTimeByPublicIdHandlerTester
{
	[Fact]
	public async Task I_Can_Delete_WorkTime_By_Its_PublicId()
	{
		//Arrange 
		Fixture fixture = new();
		fixture.Customizations.Add(new RandomDateTimeSequenceGenerator(DateTime.Now.AddYears(-2), DateTime.Now));
		string name = fixture.Create<string>();
		DateTime time = fixture.Create<DateTime>();

		using IApplicationDbContext dataAccess = this.CreateDataAccess();

		SetupHelper helper = new(dataAccess);
		WorkTime workTime = await helper.SetupWorkTimeAsync(time.ToUniversalTime());


		DeleteWorkTimeByPublicIdHandler sut = new(dataAccess);
		DeleteWorkTimeByPublicIdCommand request = new(workTime.PublicId);

		//Act
		bool results = await sut.Handle(request, CancellationToken.None);

		//Assert
		_ = results.Should().BeTrue();
		_ = (await dataAccess.WorkTime.AnyAsync(x => x.Id == workTime.Id)).Should().BeFalse();
	}
}
