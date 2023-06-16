using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    public List<string> TypeList
    {
        get
        {
            return Enum.GetNames(typeof(UserList.ListType)).ToList();
        }
    }

    [ObservableProperty]
    private UserList.ListType userListType;


    [RelayCommand]
    public async void OnUserListCompleted()
    {
        UserList = new();
        UserList.Name = UlName;
        UserList.TargetStore = UlTargetStore;
        UserList.Type = UserListType;


        UserList = _userListService.CreateUserList(UserList);




        await Shell.Current.GoToAsync("..?id=" + UserList.Id + "&createflag=true");

    }


    [RelayCommand]
    public async void OnCancel()
    {

        await Shell.Current.GoToAsync("..?createflag=false");

    }
}
