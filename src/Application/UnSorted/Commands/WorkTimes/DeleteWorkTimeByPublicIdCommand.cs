using MediatR;

namespace Application.Commands.WorkTimes;
public record DeleteWorkTimeByPublicIdCommand(Guid PublicId) : IRequest<bool>;

