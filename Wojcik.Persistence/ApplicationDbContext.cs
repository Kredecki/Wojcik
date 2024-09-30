using Microsoft.EntityFrameworkCore;
using Wojcik.Persistence.Entities;

namespace Wojcik.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        :base(options)
    {
        
    }

    public DbSet<Example> Examples { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		#region Example
		modelBuilder.Entity<Example>(model =>
		{
			model.HasKey(m => m.Guid);
			model.Property(m => m.Name).HasMaxLength(32);
			model.Property(m => m.Description).HasMaxLength(255);
		});
		#endregion
	}
}
