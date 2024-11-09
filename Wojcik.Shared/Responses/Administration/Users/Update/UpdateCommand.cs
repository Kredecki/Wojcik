using MediatR;
using Wojcik.Shared.DTOs;

namespace Wojcik.Shared.Responses.Administration.Users.Update;

public record UpdateCommand(UserDTO DTO) : IRequest;
