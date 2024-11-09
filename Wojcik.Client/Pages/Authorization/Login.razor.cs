using Radzen;
using Wojcik.Shared.Models;

namespace Wojcik.Client.Pages.Authorization;

public partial class Login
{
    private LoginModel Model { get; set; } = new LoginModel();

    private async Task OnSubmit(LoginArgs args)
    {
        Model.UserName = args.Username;
        Model.Password = args.Password;
        Model.RememberMe = args.RememberMe;

        await authStateProvider.Login(Model);
        navigationManager.NavigateTo("");
    }
}
