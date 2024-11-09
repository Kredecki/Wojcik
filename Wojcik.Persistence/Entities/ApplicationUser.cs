using Microsoft.AspNetCore.Identity;

namespace Wojcik.Persistence.Entities;

public class ApplicationUser : IdentityUser
{
	public bool Enabled { get; set; } = false;
}