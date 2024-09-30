using MediatR;
using Wojcik.Shared.Dtos;

namespace Wojcik.Shared.Request.Example.Get;

public class GetQuery : IRequest<ExampleDto>
{
}
