using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wojcik.Shared.DTOs;
using Wojcik.Shared.Request.Administration.Users.Get;
using Wojcik.Shared.Responses.Administration.Users.Update;

namespace Wojcik.Controllers;

[Route("Api/[controller]/[action]")]
[ApiController]
public class UsersController(IMediator mediator) : Controller
{
    [HttpGet]
    [Authorize(Roles = "Administrator")]
    public async Task<List<UserDTO>> Get()
        => await mediator.Send(new GetQuery());

    [HttpPost]
    [Authorize(Roles = "Administrator")]
    public async Task Update(UserDTO dto)
        => await mediator.Send(new UpdateCommand(dto));
}
