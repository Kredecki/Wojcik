namespace Wojcik.Persistence.Entities;

public abstract class AuditableEntity
{
	public Guid CreatedBy { get; set; } = Guid.Empty;
	public DateTime Created { get; set; } = DateTime.MinValue;
	public Guid ModifiedBy { get; set; } = Guid.Empty;
	public DateTime Modified { get; set; } = DateTime.MinValue;
}
