//Viewmodel для страницы ввода новой цели

namespace BucketListMAUI.ViewModel;

[QueryProperty("UserList", "UserList")]
public partial class UserListDataInputViewModel: BaseViewModel
{
    readonly UserListService _userListService;
    readonly ItemService _itemService;

    [ObservableProperty]
    UserList userList;

    [ObservableProperty]
    public string ulName;

    [ObservableProperty]
    public string ulTargetStore;

    public UserListDataInputViewModel()
    {
        _userListService = new();
        _itemService = new();
    }



    [RelayCommand]
    public async void OnUserListCompleted()
    {
        UserList = new()
        {
            Name = UlName
        };


        UserList = _userListService.CreateUserList(UserList);




        await Shell.Current.GoToAsync("..?id=" + UserList.Id + "&createflag=true");

    }


    [RelayCommand]
    public static async void OnCancel()
    {

        await Shell.Current.GoToAsync("..?createflag=false");

    }
}
