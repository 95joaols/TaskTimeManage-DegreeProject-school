﻿using Application.Common.Interfaces;
using Application.CQRS.WorkTimes.Commands;

using Ardalis.GuardClauses;

using Domain.Entities;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.WorkTimes.Handlers;
public class UpdateWorkTimesHandler : IRequestHandler<UpdateWorkTimesCommand, IEnumerable<WorkTime>>
{
	private readonly IApplicationDbContext data;

	public UpdateWorkTimesHandler(IApplicationDbContext data) => this.data = data;
	public async Task<IEnumerable<WorkTime>> Handle(UpdateWorkTimesCommand request, CancellationToken cancellationToken)
	{
		_ = Guard.Against.NullOrEmpty(request.WorkTimes);

		IEnumerable<WorkTime> WorkTimes = await data.WorkTime.Where(wt => request.WorkTimes.Select(x => x.PublicId).Contains(wt.PublicId)).ToListAsync(cancellationToken);

		foreach (WorkTime? workTime in WorkTimes)
		{
			workTime.Time = request.WorkTimes.FirstOrDefault(wt => wt.PublicId == workTime.PublicId).Time;
			workTime.Time = workTime.Time > DateTime.UtcNow ? DateTime.UtcNow : workTime.Time;
		}
		_ = await data.SaveChangesAsync(cancellationToken);
		return WorkTimes;
	}
}