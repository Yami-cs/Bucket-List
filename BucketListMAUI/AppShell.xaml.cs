using BucketListMAUI.View;

namespace BucketListMAUI;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
        Routing.RegisterRoute(nameof(UserListDetails), typeof(UserListDetails));
        Routing.RegisterRoute(nameof(UserListDataInput), typeof(UserListDataInput));
        Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
    }
}
