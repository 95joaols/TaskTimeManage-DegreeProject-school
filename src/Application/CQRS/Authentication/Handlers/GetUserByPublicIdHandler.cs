namespace Application.CQRS.Authentication.Handlers;

public class GetUserByPublicIdHandler : IRequestHandler<GetUserByPublicIdQuery, UserProfile?>
{
  private readonly IApplicationDbContext _data;

  public GetUserByPublicIdHandler(IApplicationDbContext data) => _data = data;

  public async Task<UserProfile?> Handle(GetUserByPublicIdQuery request, CancellationToken cancellationToken)
  {
    Guard.Against.Default(request.UserPublicId);

    return await _data.UserProfile.FirstOrDefaultAsync(u => u.PublicId == request.UserPublicId, cancellationToken);
  }
}