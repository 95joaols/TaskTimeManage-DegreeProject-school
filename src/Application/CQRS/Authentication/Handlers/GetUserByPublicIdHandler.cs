using Application.Common.Interfaces;
using Application.CQRS.Authentication.Queries;


using Ardalis.GuardClauses;

using Domain.Aggregates.UserAggregate;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Authentication.Handlers;
public class GetUserByPublicIdHandler : IRequestHandler<GetUserByPublicIdQuery, UserProfile?>
{
  private readonly IApplicationDbContext data;

  public GetUserByPublicIdHandler(IApplicationDbContext data) => this.data = data;

  public async Task<UserProfile?> Handle(GetUserByPublicIdQuery request, CancellationToken cancellationToken)
  {
    _ = Guard.Against.Default(request.UserPublicId);

    return await data.UserProfile.FirstOrDefaultAsync(u => u.PublicId == request.UserPublicId, cancellationToken);
  }
}
