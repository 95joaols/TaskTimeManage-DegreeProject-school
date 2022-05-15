using Application.Commands.WorkItems;
using Application.DataAccess;
using Application.Models;

using Ardalis.GuardClauses;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Application.Handlers.WorkItems;
public class UpdateWorkItemHandler : IRequestHandler<UpdateWorkItemCommand, WorkItemModel>
{
	private readonly TTMDataAccess data;

	public UpdateWorkItemHandler(TTMDataAccess data) => this.data = data;

	public async Task<WorkItemModel> Handle(UpdateWorkItemCommand request, CancellationToken cancellationToken)
	{
		Guard.Against.Default(request.PublicId);

		Guard.Against.NullOrWhiteSpace(request.Name);


		WorkItemModel? workItem = await data.WorkItem.FirstOrDefaultAsync(wi => wi.PublicId == request.PublicId, cancellationToken: cancellationToken);

		Guard.Against.Null(workItem);


		workItem.Name = request.Name.Trim();
		_ = await data.SaveChangesAsync(cancellationToken);
		return workItem;
	}
}
