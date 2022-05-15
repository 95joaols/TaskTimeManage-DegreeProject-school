using Application.Models;

using MediatR;

namespace Application.Commands.WorkTimes;
public record CreateWorkTimeCommand(DateTime Time, Guid WorkItemPublicId) : IRequest<WorkTimeModel>;

