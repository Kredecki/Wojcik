using Radzen;
using System.Security.Claims;

namespace Wojcik.Client.Components;

public partial class AdminNavigation
{
	private ClaimsPrincipal user = new();
	private MenuItemDisplayStyle DisplayStyle = MenuItemDisplayStyle.IconAndText;
	private bool isShowArrow = true;
	private string sidebar = "sidebar-expanded";
	private string subSidebar = "sub-sidebar-expanded";

	protected override async Task OnInitializedAsync()
	{
		var authState = await authStateProvider.GetAuthenticationStateAsync();
		this.user = authState.User;
	}

	private void ToggleMenu()
	{
		if (DisplayStyle == MenuItemDisplayStyle.IconAndText)
		{
			DisplayStyle = MenuItemDisplayStyle.Icon;
			isShowArrow = false;
			sidebar = "sidebar-collapsed";
			subSidebar = "sub-sidebar-collapsed";
		}
		else
		{
			DisplayStyle = MenuItemDisplayStyle.IconAndText;
			isShowArrow = true;
			sidebar = "sidebar-expanded";
			subSidebar = "sub-sidebar-expanded";
		}
	}
}
