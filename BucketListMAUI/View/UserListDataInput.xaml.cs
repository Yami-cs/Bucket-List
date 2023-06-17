namespace BucketListMAUI.View;

public partial class UserListDataInput : ContentPage
{
    private readonly UserListDataInputViewModel _uldiv;

    public UserListDataInput(UserListDataInputViewModel userListDataInputViewModel)
    {
        InitializeComponent();
        BindingContext = userListDataInputViewModel;
        _uldiv = userListDataInputViewModel;

    }

    private async void NewItemButtonPressed(object sender, EventArgs e)
    {
        var circularButton = sender as CircularButton;
        await circularButton.BounceOnPressAsync();
        _uldiv.OnUserListCompleted();

    }
}