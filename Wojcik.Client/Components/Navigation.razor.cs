namespace Wojcik.Client.Components;

public partial class Navigation
{
    private readonly object _label = "Shop";
    private string SearchValue = string.Empty;

    private async Task Logout()
    {
        await authStateProvider.Logout();
        navigationManager.NavigateTo("/login");
    }
}
