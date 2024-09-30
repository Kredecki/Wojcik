namespace Wojcik.Persistence.Entities;

public class Example : AuditableEntity
{
	public Guid Guid { get; set; } = Guid.Empty;
	public string Name { get; set; } = string.Empty;
	public string Description { get; set; } = string.Empty;
}
