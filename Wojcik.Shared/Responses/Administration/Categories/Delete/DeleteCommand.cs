using MediatR;

namespace Wojcik.Shared.Responses.Administration.Categories.Delete;

public record DeleteCommand(Guid Id) : IRequest;
