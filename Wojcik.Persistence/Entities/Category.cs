namespace Wojcik.Persistence.Entities;

public class Category : AuditableEntity
{
    public Guid Id { get; set; } = Guid.Empty;
    public string Name { get; set; } = string.Empty;
    public bool Enabled { get; set; } = false;

    public ICollection<Product> Products { get; set; } = [];
}
