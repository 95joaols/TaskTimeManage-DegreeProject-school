using Application.Common.Interfaces;
using Application.CQRS.WorkTimes.Commands;

using Ardalis.GuardClauses;

using MediatR;

namespace Application.CQRS.WorkTimes.Handlers;
public class DeleteAllWorkTimesByWorkItemIdHandler : IRequestHandler<DeleteAllWorkTimesByWorkItemIdCommand, bool>
{

	private readonly IApplicationDbContext data;

	public DeleteAllWorkTimesByWorkItemIdHandler(IApplicationDbContext data) => this.data = data;

	public async Task<bool> Handle(DeleteAllWorkTimesByWorkItemIdCommand request, CancellationToken cancellationToken)
	{
		_ = Guard.Against.NegativeOrZero(request.WorkItemId);

		data.WorkTime.RemoveRange(data.WorkTime.Where(wt => wt.WorkItemId == request.WorkItemId));

		_ = await data.SaveChangesAsync(cancellationToken);
		return true;
	}
}
