using Microsoft.EntityFrameworkCore;
using Wojcik.Shared.Dtos;
using Wojcik.Shared.Interfaces.Repositories;

namespace Wojcik.Persistence.Repositories;

public class ExampleRepository(ApplicationDbContext db) : IExampleRepository
{
	public async Task<ExampleDto> Get(CancellationToken cancellationToken)
	{
		try
		{
			return await db.Examples
			.Select(x => new ExampleDto
			{
				GuId = x.Guid,
				Name = x.Name,
				Description = x.Description
			}).FirstOrDefaultAsync(cancellationToken) ?? new ExampleDto();
		}
		catch(Exception ex)
		{
			throw new Exception(ex.Message);
		}
	}
}
