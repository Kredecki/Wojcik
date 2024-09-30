using MediatR;
using Microsoft.AspNetCore.Mvc;
using Wojcik.Shared.Dtos;
using Wojcik.Shared.Request.Example.Get;

namespace Wojcik.Controllers;

[Route("Api/Example/")]
public class ExampleController(IMediator mediator) : Controller
{
    [HttpGet("Get")]
    public async Task<ExampleDto> Get()
    {
        try
        {
            return await mediator.Send(new GetQuery());
        }
        catch(Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
