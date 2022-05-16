using Application.Common.Interfaces;
using Application.CQRS.WorkItems.Queries;
using Application.CQRS.WorkTimes.Commands;

using Ardalis.GuardClauses;

using Domain.Entities;

using MediatR;

namespace Application.CQRS.WorkTimes.Handlers;
public class CreateWorkTimeHandler : IRequestHandler<CreateWorkTimeCommand, WorkTime>
{
	private readonly IApplicationDbContext data;
	private readonly IMediator mediator;

	public CreateWorkTimeHandler(IApplicationDbContext data, IMediator mediator)
	{
		this.data = data;
		this.mediator = mediator;
	}

	public async Task<WorkTime> Handle(CreateWorkTimeCommand request, CancellationToken cancellationToken)
	{
		_ = Guard.Against.Default(request.WorkItemPublicId);

		WorkItem? WorkItem = await mediator.Send(new GetWorkItemWithWorkTimeByPublicIdQuery(request.WorkItemPublicId), cancellationToken);
		_ = Guard.Against.Null(WorkItem);


		WorkTime WorkTime = new() {
			Time = request.Time,
			WorkItem = WorkItem
		};
		_ = data.WorkTime.Add(WorkTime);
		_ = await data.SaveChangesAsync(cancellationToken);

		return WorkTime;
	}
}
