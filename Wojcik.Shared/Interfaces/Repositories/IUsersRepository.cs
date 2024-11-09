using Wojcik.Shared.DTOs;

namespace Wojcik.Shared.Interfaces.Repositories;

public interface IUsersRepository
{
	Task<List<UserDTO>> Get(CancellationToken cancellationToken);
	Task Update(UserDTO DTO, CancellationToken cancellationToken);
}
