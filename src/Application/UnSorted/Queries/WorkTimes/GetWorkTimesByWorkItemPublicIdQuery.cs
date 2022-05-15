using Application.Models;

using MediatR;

namespace Application.Queries.WorkTimes;
public record GetWorkTimesByWorkItemPublicIdQuery(Guid PublicId) : IRequest<IEnumerable<WorkTimeModel>>;

