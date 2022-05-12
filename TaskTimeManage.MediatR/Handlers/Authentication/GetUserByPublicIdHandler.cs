using MediatR;

using Microsoft.EntityFrameworkCore;

using TaskTimeManage.MediatR.DataAccess;
using TaskTimeManage.MediatR.Models;
using TaskTimeManage.MediatR.Queries.Authentication;

namespace TaskTimeManage.MediatR.Handlers.Authentication;
public class GetUserByPublicIdHandler : IRequestHandler<GetUserByPublicIdQuery, UserModel?>
{
	private readonly TTMDataAccess data;

	public GetUserByPublicIdHandler(TTMDataAccess data) => this.data = data;

	public async Task<UserModel?> Handle(GetUserByPublicIdQuery request, CancellationToken cancellationToken)
	{
		if (request.UserPublicId == Guid.Empty)
		{
			throw new ArgumentNullException($"'{nameof(request.UserPublicId)}' cannot be null or whitespace.", nameof(request.UserPublicId));
		}
		return await data.User.FirstOrDefaultAsync(u => u.PublicId == request.UserPublicId, cancellationToken);
	}
}
