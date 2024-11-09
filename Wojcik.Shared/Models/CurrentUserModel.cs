namespace Wojcik.Shared.Models;

public class CurrentUserModel
{
    public bool IsAuthenticated { get; set; } = false;
    public string UserName { get; set; } = string.Empty;
	public List<string> Roles { get; set; } = [];
	public Dictionary<string, string> Claims { get; set; } = [];
}