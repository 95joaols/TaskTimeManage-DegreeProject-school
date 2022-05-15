using Domain.Entities;

using MediatR;

namespace Application.CQRS.Authentication.Queries;
public record GetUserByPublicIdQuery(Guid UserPublicId) : IRequest<User?>;

