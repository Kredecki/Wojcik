using Wojcik.Shared.Models;

namespace Wojcik.Client.Interfaces;

public interface IAuthService
{
    Task Login(LoginModel loginRequest);
    Task Register(RegisterModel registerRequest);
    Task Logout();
    Task<CurrentUserModel> CurrentUserInfo();
}