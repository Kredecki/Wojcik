using MediatR;
using Wojcik.Shared.Interfaces.Repositories;

namespace Wojcik.Shared.Responses.Administration.Categories.Update;

public class UpdateHandler(ICategoriesRepository categoriesRepository) : IRequestHandler<UpdateCommand>
{
    public async Task Handle(UpdateCommand command, CancellationToken cancellationToken)
        => await categoriesRepository.Update(command.DTO, cancellationToken);
}
