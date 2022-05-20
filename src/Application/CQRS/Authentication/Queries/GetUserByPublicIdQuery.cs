namespace Application.CQRS.Authentication.Queries;

public record GetUserByPublicIdQuery(Guid UserPublicId) : IRequest<UserProfile?>;