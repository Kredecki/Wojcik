using MediatR;
using Wojcik.Shared.Interfaces.Repositories;

namespace Wojcik.Shared.Responses.Administration.Categories.Create;

public class CreateHandler(ICategoriesRepository categoriesRepository) : IRequestHandler<CreateCommand>
{
    public async Task Handle(CreateCommand command, CancellationToken cancellationToken)
        => await categoriesRepository.Create(command.DTO, cancellationToken);
}
