using Wojcik.Shared.Models;

namespace Wojcik.Client.Interfaces;

public interface IAuthService
{
    Task Login(LoginRequest loginRequest);
    Task Register(RegisterRequest registerRequest);
    Task Logout();
    Task<CurrentUser> CurrentUserInfo();
}