using Application.DataAccess;
using Application.Models;
using Application.Queries.Authentication;

using Ardalis.GuardClauses;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Application.Handlers.Authentication;
public class GetUserByPublicIdHandler : IRequestHandler<GetUserByPublicIdQuery, UserModel?>
{
	private readonly TTMDataAccess data;

	public GetUserByPublicIdHandler(TTMDataAccess data) => this.data = data;

	public async Task<UserModel?> Handle(GetUserByPublicIdQuery request, CancellationToken cancellationToken)
	{
		Guard.Against.Default(request.UserPublicId);

		return await data.User.FirstOrDefaultAsync(u => u.PublicId == request.UserPublicId, cancellationToken);
	}
}
