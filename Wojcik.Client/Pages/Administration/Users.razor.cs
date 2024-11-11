using Radzen;
using System;
using System.Net.Http.Json;
using Wojcik.Shared.DTOs;

namespace Wojcik.Client.Pages.Administration;

public partial class Users
{
    private List<UserDTO> data = [];

    protected override async Task OnInitializedAsync()
    {
        client = httpClientFactory.CreateClient("Wojcik");
        await Get();
    }

    private async Task Get()
    {
        data = await client.GetFromJsonAsync<List<UserDTO>>("Api/Users/Get") ?? [];
    }

    private async void Update(Guid id)
    {
        var user = data.Where(x => x.Id == id).Select(x => x).First();
        string state = string.Empty;
        if (user.Enabled)
        {
            state = "nałożona";
            user.Enabled = false;
        }
        else
        {
            state = "zdjęta";
            user.Enabled = true;
        }

        await client.PostAsJsonAsync<UserDTO>("Api/Users/Update", user);

        ShowNotification(new NotificationMessage
        {
            Style = "position: absolute; inset-inline-start: -1000px;",
            Severity = NotificationSeverity.Success,
            Summary = $"Blokada {state} pomyślnie.",
            Detail = "",
            Duration = 4000
        });

        await Get();
    }

    private void ShowNotification(NotificationMessage message)
        => notificationService.Notify(message);   
}
