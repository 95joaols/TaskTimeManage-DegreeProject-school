using Application.Dto;
using Application.Models;

using MediatR;

namespace Application.Commands.WorkTimes;
public record UpdateWorkTimesCommand(IEnumerable<WorkTimesLight> WorkTimes) : IRequest<IEnumerable<WorkTimeModel>>;
