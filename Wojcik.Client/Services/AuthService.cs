using System.Net.Http.Json;
using Wojcik.Client.Interfaces;
using Wojcik.Shared.Models;

namespace Wojcik.Client.Services;

public class AuthService(IHttpClientFactory httpClientFactory) : IAuthService
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient("Wojcik");

    public async Task<CurrentUserModel> CurrentUserInfo()
        => await _httpClient.GetFromJsonAsync<CurrentUserModel>("Api/Auth/CurrentUserInfo") ?? new CurrentUserModel();

    public async Task Login(LoginModel loginRequest)
    {
        var result = await _httpClient.PostAsJsonAsync("Api/Auth/Login", loginRequest);
        if (result.StatusCode == System.Net.HttpStatusCode.BadRequest) throw new Exception(await result.Content.ReadAsStringAsync());

        result.EnsureSuccessStatusCode();
    }

    public async Task Logout()
    {
        var result = await _httpClient.PostAsync("Api/Auth/logout", null);
        result.EnsureSuccessStatusCode();
    }

    public async Task Register(RegisterModel registerRequest)
    {
        var result = await _httpClient.PostAsJsonAsync("Api/Auth/Register", registerRequest);
        if (result.StatusCode == System.Net.HttpStatusCode.BadRequest) throw new Exception(await result.Content.ReadAsStringAsync());

        result.EnsureSuccessStatusCode();
    }
}