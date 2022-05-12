using MediatR;

namespace TaskTimeManage.Core.Commands.WorkTimes;
public record DeleteWorkTimeByPublicIdCommand(Guid PublicId) : IRequest<bool>;

