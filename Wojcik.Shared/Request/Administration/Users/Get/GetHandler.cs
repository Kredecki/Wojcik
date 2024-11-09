using MediatR;
using Wojcik.Shared.DTOs;
using Wojcik.Shared.Interfaces.Repositories;

namespace Wojcik.Shared.Request.Administration.Users.Get;

public class GetHandler(IUsersRepository usersRepository) : IRequestHandler<GetQuery, List<UserDTO>>
{
	public async Task<List<UserDTO>> Handle(GetQuery query, CancellationToken cancellationToken)
		=> await usersRepository.Get(cancellationToken);
}
