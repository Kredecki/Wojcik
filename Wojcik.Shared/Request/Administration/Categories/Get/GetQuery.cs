using MediatR;
using Wojcik.Shared.DTOs;

namespace Wojcik.Shared.Request.Administration.Categories.Get;

public class GetQuery : IRequest<List<CategoryDTO>> { }
