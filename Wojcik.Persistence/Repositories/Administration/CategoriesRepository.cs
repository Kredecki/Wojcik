using Microsoft.EntityFrameworkCore;
using Wojcik.Persistence.Entities;
using Wojcik.Shared.DTOs;
using Wojcik.Shared.Interfaces.Repositories;

namespace Wojcik.Persistence.Repositories.Administration;

public class CategoriesRepository(ApplicationDbContext db) : ICategoriesRepository
{
    public async Task<List<CategoryDTO>> Get(CancellationToken cancellationToken)
    {
        return await (from c in db.Categories
                      select new CategoryDTO
                      {
                          Id = c.Id,
                          Name = c.Name,
                          Enabled = c.Enabled
                      }).ToListAsync(cancellationToken) ?? [];
    }

    public async Task Create(CategoryDTO DTO, CancellationToken cancellationToken)
    {
        Category category = new()
        {
            Id = DTO.Id,
            Name = DTO.Name,
            Enabled = DTO.Enabled,
            Created = DateTime.Now,
            CreatedBy = DTO.UserId,
            Modified = DateTime.Now,
            ModifiedBy = DTO.UserId
        };

        await db.AddAsync(category, cancellationToken);
        await db.SaveChangesAsync(cancellationToken);
    }

    public async Task Update(CategoryDTO DTO, CancellationToken cancellationToken)
    {
        var category = await (from c in db.Categories
                              where c.Id == DTO.Id
                              select c).FirstOrDefaultAsync(cancellationToken);
        if (category is null) return;

        category.Name = DTO.Name;
        category.Enabled = DTO.Enabled;
        category.Modified = DateTime.Now;
        category.ModifiedBy = DTO.UserId;

        await db.SaveChangesAsync(cancellationToken);
    }

    public async Task Delete(Guid id, CancellationToken cancellationToken)
    {
        var category = await (from c in db.Categories
                              where c.Id == id
                              select c).FirstOrDefaultAsync(cancellationToken);
        if (category is null) return;

        db.Remove(category);
        await db.SaveChangesAsync(cancellationToken);
    }
}
