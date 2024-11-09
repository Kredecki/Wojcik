using Microsoft.EntityFrameworkCore;
using Wojcik.Shared.DTOs;
using Wojcik.Shared.Interfaces.Repositories;

namespace Wojcik.Persistence.Repositories.Administration;

public class UsersRepository(ApplicationDbContext db) : IUsersRepository
{
	public async Task<List<UserDTO>> Get(CancellationToken cancellationToken)
	{
		return await (from ur in db.UserRoles
					  join u in db.Users on ur.UserId equals u.Id
					  join r in db.Roles on ur.RoleId equals r.Id
					  select new UserDTO
					  {
						  Id = Guid.Parse(u.Id),
						  UserName = u.UserName!,
						  Email = u.Email!,
						  Role = r.Name!,
						  Enabled = u.Enabled
					  })
					  .AsNoTracking()
					  .ToListAsync(cancellationToken) ?? [];
	}

	public async Task Update(UserDTO DTO, CancellationToken cancellationToken)
	{
		var user = await (from u in db.Users
						  where u.Id == DTO.Id.ToString()
						  select u)
						  .FirstOrDefaultAsync(cancellationToken);
		if (user is null) return;

		user.Enabled = DTO.Enabled;

		await db.SaveChangesAsync(cancellationToken);
	}
}
