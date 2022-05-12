using MediatR;

using TaskTimeManage.MediatR.Models;

namespace TaskTimeManage.MediatR.Queries.Authentication;
public record GetUserByPublicIdQuery(Guid UserPublicId) : IRequest<UserModel?>;

