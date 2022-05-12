using MediatR;

using TaskTimeManage.Core.Models;

namespace TaskTimeManage.Core.Queries.Authentication;
public record GetUserByPublicIdQuery(Guid UserPublicId) : IRequest<UserModel?>;

