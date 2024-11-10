using Microsoft.AspNetCore.Components.Authorization;

namespace Wojcik.Client.Components;

public partial class Navigation
{
    private readonly object _label = "Shop";
    private string SearchValue = string.Empty;
    private bool isAdmin = false;

    protected override async Task OnInitializedAsync()
    {
        var authState = await authStateProvider.GetAuthenticationStateAsync();
        isAdmin = authState.User.IsInRole("Administrator");
    }

    private async Task Logout()
    {
        await authStateProvider.Logout();
        navigationManager.NavigateTo("/login");
    }
}
