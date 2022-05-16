﻿using Domain.Entities;

using MediatR;

namespace Application.CQRS.WorkTimes.Queries;
public record GetWorkTimesByWorkItemPublicIdQuery(Guid PublicId) : IRequest<IEnumerable<WorkTime>>;
