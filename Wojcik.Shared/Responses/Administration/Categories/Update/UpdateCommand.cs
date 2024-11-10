using MediatR;
using Wojcik.Shared.DTOs;

namespace Wojcik.Shared.Responses.Administration.Categories.Update;

public record UpdateCommand(CategoryDTO DTO) : IRequest;
