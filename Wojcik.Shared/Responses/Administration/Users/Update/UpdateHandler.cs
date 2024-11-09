using MediatR;
using Wojcik.Shared.Interfaces.Repositories;

namespace Wojcik.Shared.Responses.Administration.Users.Update;

public class UpdateHandler(IUsersRepository usersRepository) : IRequestHandler<UpdateCommand>
{
    public async Task Handle(UpdateCommand command, CancellationToken cancellationToken)
        => await usersRepository.Update(command.DTO, cancellationToken);
}
