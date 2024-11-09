namespace Wojcik.Client.Components;

public partial class Navigation
{
    private object _label = "Shop";

    private async Task Logout()
    {
        await authStateProvider.Logout();
        navigationManager.NavigateTo("/login");
    }
}
