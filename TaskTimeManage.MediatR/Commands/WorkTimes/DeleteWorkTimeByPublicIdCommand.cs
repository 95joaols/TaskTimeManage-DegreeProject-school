using MediatR;

namespace TaskTimeManage.MediatR.Commands.WorkTimes;
public record DeleteWorkTimeByPublicIdCommand(Guid PublicId) : IRequest<bool>;

