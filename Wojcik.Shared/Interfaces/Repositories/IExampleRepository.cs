using Wojcik.Shared.Dtos;

namespace Wojcik.Shared.Interfaces.Repositories;

public interface IExampleRepository
{
	Task<ExampleDto> Get(CancellationToken cancellationToken);
}
