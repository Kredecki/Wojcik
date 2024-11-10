using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wojcik.Shared.DTOs;
using Wojcik.Shared.Request.Administration.Categories.Get;
using Wojcik.Shared.Responses.Administration.Categories.Create;
using Wojcik.Shared.Responses.Administration.Categories.Delete;
using Wojcik.Shared.Responses.Administration.Categories.Update;

namespace Wojcik.Controllers;

[Route("Api/[controller]/[action]")]
[ApiController]
public class CategoriesController(IMediator mediator) : Controller
{
    [HttpGet]
    public async Task<List<CategoryDTO>> Get()
        => await mediator.Send(new GetQuery());

    [HttpPost]
    [Authorize(Roles = "Administrator")]
    public async Task Create(CategoryDTO DTO)
    {
        DTO.UserId = Guid.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value!);
        await mediator.Send(new CreateCommand(DTO));
    }

    [HttpPost]
    [Authorize(Roles = "Administrator")]
    public async Task Update(CategoryDTO DTO)
    {
        DTO.UserId = Guid.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value!);
        await mediator.Send(new UpdateCommand(DTO));
    }

    [HttpDelete]
    [Authorize(Roles = "Administrator")]
    public async Task Delete(Guid id)
        => await mediator.Send(new DeleteCommand(id));
}
