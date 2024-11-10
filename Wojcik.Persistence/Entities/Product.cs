namespace Wojcik.Persistence.Entities;

public class Product : AuditableEntity
{
    public Guid Id { get; set; } = Guid.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; } = default;
    public int stock { get; set; } = default;
    public bool Enabled { get; set; } = false;

    public ICollection<Category> Categories { get; set; } = [];
}
