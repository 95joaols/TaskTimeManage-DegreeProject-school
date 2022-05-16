using Domain.Entities;

using MediatR;

namespace Application.CQRS.WorkTimes.Commands;
public record CreateWorkTimeCommand(DateTime Time, Guid WorkItemPublicId) : IRequest<WorkTime>;

