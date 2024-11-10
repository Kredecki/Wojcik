using MediatR;
using Wojcik.Shared.DTOs;

namespace Wojcik.Shared.Responses.Administration.Categories.Create;

public record CreateCommand(CategoryDTO DTO) : IRequest;