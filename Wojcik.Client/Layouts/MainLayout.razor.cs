using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;

namespace Wojcik.Client.Layouts;

public partial class MainLayout
{
	[CascadingParameter]
	Task<AuthenticationState> AuthenticationState { get; set; } = default!;

	protected override async Task<Task> OnInitializedAsync()
	{
		if (!(await AuthenticationState).User.Identity!.IsAuthenticated)
		{
			navigationManager.NavigateTo("/login");
		}

		return base.OnInitializedAsync();
	}
}
