using MediatR;
using Wojcik.Shared.DTOs;
using Wojcik.Shared.Interfaces.Repositories;

namespace Wojcik.Shared.Request.Administration.Categories.Get;

public class GetHandler(ICategoriesRepository categoriesRepository) : IRequestHandler<GetQuery, List<CategoryDTO>>
{
    public async Task<List<CategoryDTO>> Handle(GetQuery query, CancellationToken cancellationToken)
        => await categoriesRepository.Get(cancellationToken);
}
