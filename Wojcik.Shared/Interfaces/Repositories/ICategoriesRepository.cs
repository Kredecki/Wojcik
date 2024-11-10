using Wojcik.Shared.DTOs;

namespace Wojcik.Shared.Interfaces.Repositories;

public interface ICategoriesRepository
{
    Task<List<CategoryDTO>> Get(CancellationToken cancellationToken);
    Task Create(CategoryDTO DTO, CancellationToken cancellationToken);
    Task Update(CategoryDTO DTO, CancellationToken cancellationToken);
    Task Delete(Guid id, CancellationToken cancellationToken);
}
