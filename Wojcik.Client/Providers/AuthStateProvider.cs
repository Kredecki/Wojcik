using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using Wojcik.Client.Interfaces;
using Wojcik.Shared.Models;

namespace Wojcik.Client.Providers;

public class AuthStateProvider(IAuthService api) : AuthenticationStateProvider
{
    private readonly IAuthService api = api;
    private CurrentUserModel _currentUser;

	public override async Task<AuthenticationState> GetAuthenticationStateAsync()
	{
		var identity = new ClaimsIdentity();
		try
		{
			var userInfo = await GetCurrentUser();
			if (userInfo.IsAuthenticated)
			{
				var claims = new[]
				{
					new Claim(ClaimTypes.Name, _currentUser!.UserName)
				}
				.Concat(userInfo.Roles.Select(role => new Claim(ClaimTypes.Role, role)))
				.Concat(_currentUser.Claims.Select(c => new Claim(c.Key, c.Value)));
				identity = new ClaimsIdentity(claims, "Server authentication");
			}
		}
		catch (HttpRequestException ex)
		{
			Console.WriteLine("Request failed: " + ex.ToString());
		}

		return new AuthenticationState(new ClaimsPrincipal(identity));
	}

	private async Task<CurrentUserModel> GetCurrentUser()
    {
        if (_currentUser != null && _currentUser.IsAuthenticated) return _currentUser;
        _currentUser = await api.CurrentUserInfo();
        return _currentUser;
    }
    public async Task Logout()
    {
        await api.Logout();
        _currentUser = null;
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }
    public async Task Login(LoginModel loginParameters)
    {
        await api.Login(loginParameters);
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }
    public async Task Register(RegisterModel registerParameters)
    {
        await api.Register(registerParameters);
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }
}