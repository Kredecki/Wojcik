using Radzen;
using Radzen.Blazor;
using System.Net.Http.Json;
using Wojcik.Shared.DTOs;

namespace Wojcik.Client.Pages.Administration;

public partial class Categories
{
    private List<CategoryDTO> data = [];
    private RadzenDataGrid<CategoryDTO> categoriesGrid = new();
    private readonly List<CategoryDTO> categoriesToInsert = [];
    private readonly List<CategoryDTO> categoriesToUpdate = [];

    protected override async Task OnInitializedAsync()
    {
        client = httpClientFactory.CreateClient("Wojcik");
        await Get();
    }

    private async Task Get()
    {
        data = await client.GetFromJsonAsync<List<CategoryDTO>>("Api/Categories/Get") ?? [];
    }

    private void Reset()
    {
        categoriesToInsert.Clear();
        categoriesToUpdate.Clear();
    }

    private void Reset(CategoryDTO DTO)
    {
        categoriesToInsert.Remove(DTO);
        categoriesToUpdate.Remove(DTO);
    }

    private async Task EditRow(CategoryDTO DTO)
    {
        Reset();

        categoriesToUpdate.Add(DTO);
        await categoriesGrid.EditRow(DTO);
    }

    private async void OnUpdateRow(CategoryDTO DTO)
    {
        Reset(DTO);

        await client.PostAsJsonAsync<CategoryDTO>("Api/Categories/Update", DTO);
    }

    private async Task SaveRow(CategoryDTO DTO)
    {
        await categoriesGrid.UpdateRow(DTO);
    }

    private async void CancelEdit(CategoryDTO DTO)
    {
        Reset(DTO);

        categoriesGrid.CancelEditRow(DTO);
        await Get();
    }

    private async Task InsertRow()
    {
        Reset();

        var order = new CategoryDTO();
        categoriesToInsert.Add(order);
        await categoriesGrid.InsertRow(order);
    }

    private async void OnCreateRow(CategoryDTO DTO)
    {
        DTO.Id = Guid.NewGuid();
        await client.PostAsJsonAsync<CategoryDTO>("Api/Categories/Create", DTO);
        categoriesToInsert.Remove(DTO);
    }

    private async void DeleteRow(CategoryDTO DTO)
    {
        Reset(DTO);

        await client.DeleteAsync($"Api/Categories/Delete?id={DTO.Id}");
        await Get();
        StateHasChanged();
    }

    private async void Update(Guid guid)
    {
        var user = data.Where(x => x.Id == guid).Select(x => x).First();
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

        await client.PostAsJsonAsync<CategoryDTO>("Api/Categories/Update", user);

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
