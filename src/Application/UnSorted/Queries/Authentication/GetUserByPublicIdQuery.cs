using Application.Models;

using MediatR;

namespace Application.Queries.Authentication;
public record GetUserByPublicIdQuery(Guid UserPublicId) : IRequest<UserModel?>;

