using MediatR;
using Wojcik.Shared.Dtos;
using Wojcik.Shared.Interfaces.Repositories;

namespace Wojcik.Shared.Request.Example.Get;

public class GetHandler(IExampleRepository exampleRepository) : IRequestHandler<GetQuery, ExampleDto>
{
    public async Task<ExampleDto> Handle(GetQuery request, CancellationToken cancellationToken)
	{
		try
		{
			return await exampleRepository.Get(cancellationToken);
		}
		catch (Exception ex)
		{
			throw new Exception(ex.Message);
		}
	}
}
