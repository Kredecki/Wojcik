namespace Wojcik.Shared.Models;

public class CurrentUserModel
{
    public bool IsAuthenticated { get; set; } = false;
    public string UserName { get; set; } = string.Empty;
    public Dictionary<string, string> Claims { get; set; } = [];
}