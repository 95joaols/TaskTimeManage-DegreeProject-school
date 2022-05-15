using Application.Common.Interfaces;
using Application.CQRS.WorkItems.Commands;

using Ardalis.GuardClauses;

using Domain.Entities;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.WorkItems.Handlers;
public class UpdateWorkItemHandler : IRequestHandler<UpdateWorkItemCommand, WorkItem>
{
	private readonly IApplicationDbContext data;

	public UpdateWorkItemHandler(IApplicationDbContext data) => this.data = data;

	public async Task<WorkItem> Handle(UpdateWorkItemCommand request, CancellationToken cancellationToken)
	{
		Guard.Against.Default(request.PublicId);

		Guard.Against.NullOrWhiteSpace(request.Name);


		WorkItem? workItem = await data.WorkItem.FirstOrDefaultAsync(wi => wi.PublicId == request.PublicId, cancellationToken: cancellationToken);

		Guard.Against.Null(workItem);


		workItem.Name = request.Name.Trim();
		_ = await data.SaveChangesAsync(cancellationToken);
		return workItem;
	}
}
