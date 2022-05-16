using Application.Common.Models.Generated;

using Domain.Entities;

using MediatR;

namespace Application.CQRS.WorkTimes.Commands;
public record UpdateWorkTimesCommand(IEnumerable<WorkTimeDto> WorkTimes) : IRequest<IEnumerable<WorkTime>>;
