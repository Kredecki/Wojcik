using MediatR;
using Wojcik.Shared.Interfaces.Repositories;

namespace Wojcik.Shared.Responses.Administration.Categories.Delete;

public class DeleteHandler(ICategoriesRepository categoriesRepository) : IRequestHandler<DeleteCommand>
{
    public async Task Handle(DeleteCommand command, CancellationToken cancellationToken)
        => await categoriesRepository.Delete(command.Id, cancellationToken);
}
