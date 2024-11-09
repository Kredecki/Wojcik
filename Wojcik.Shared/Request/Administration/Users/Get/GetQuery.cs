using MediatR;
using Wojcik.Shared.DTOs;

namespace Wojcik.Shared.Request.Administration.Users.Get;

public class GetQuery : IRequest<List<UserDTO>> { }
