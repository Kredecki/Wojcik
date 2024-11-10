namespace Wojcik.Shared.DTOs;

public class CategoryDTO
{
    public Guid Id { get; set; } = Guid.Empty;
    public string Name { get; set; } = string.Empty;
    public Guid UserId { get; set; } = Guid.Empty;
    public bool Enabled { get; set; } = false;
}
