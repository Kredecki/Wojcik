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

    async Task EditRow(CategoryDTO DTO)
    {
        Reset();

        categoriesToUpdate.Add(DTO);
        await categoriesGrid.EditRow(DTO);
    }

    async void OnUpdateRow(CategoryDTO DTO)
    {
        Reset(DTO);

        await client.PostAsJsonAsync<CategoryDTO>("Api/Categories/Update", DTO);
    }

    async Task SaveRow(CategoryDTO DTO)
    {
        await categoriesGrid.UpdateRow(DTO);
    }

    async void CancelEdit(CategoryDTO DTO)
    {
        Reset(DTO);

        categoriesGrid.CancelEditRow(DTO);
        await Get();
    }

    async Task InsertRow()
    {
        Reset();

        var order = new CategoryDTO();
        categoriesToInsert.Add(order);
        await categoriesGrid.InsertRow(order);
    }

    async void OnCreateRow(CategoryDTO DTO)
    {
        DTO.Id = Guid.NewGuid();
        await client.PostAsJsonAsync<CategoryDTO>("Api/Categories/Create", DTO);
        categoriesToInsert.Remove(DTO);
    }

    async void DeleteRow(CategoryDTO DTO)
    {
        Reset(DTO);

        await client.DeleteAsync($"Api/Categories/Delete?id={DTO.Id}");
        await Get();
        StateHasChanged();
    }
}
