using System.Net.Http.Json;
using Wojcik.Shared.DTOs;

namespace Wojcik.Client.Components;

public partial class Navigation
{
    private readonly object _label = "Shop";
    private string SearchValue = string.Empty;
    private bool isAdmin = false;

    private List<CategoryDTO> categories = [];

    protected override async Task OnInitializedAsync()
    {
        client = httpClientFactory.CreateClient("Wojcik");

        var authState = await authStateProvider.GetAuthenticationStateAsync();
        isAdmin = authState.User.IsInRole("Administrator");
        await Get();
    }

    private async Task Get()
    {
        categories = await client.GetFromJsonAsync<List<CategoryDTO>>("Api/Categories/Get") ?? [];
    }

    private async Task Logout()
    {
        await authStateProvider.Logout();
        navigationManager.NavigateTo("/login");
    }
}
