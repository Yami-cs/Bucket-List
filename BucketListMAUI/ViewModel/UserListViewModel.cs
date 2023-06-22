using System.Collections.ObjectModel;
using System.Diagnostics;
using BucketListMAUI.View;

//ViewModel для основной страницы

namespace BucketListMAUI.ViewModel;

[QueryProperty("CreateFlag", "createflag")]
[QueryProperty("NewListId", "id")]
public partial class UserListViewModel : BaseViewModel
{
    private readonly UserListService _uls;
    private readonly ItemService _itemService;
    private int _newListId;

    [ObservableProperty]
    public bool isRefreshing;

    public ObservableCollection<UserList> UserLists { get; set; } = new();

    public bool CreateFlag { get; set; } = false;

    //TODO there is probably a better way to do this? idk
    public int NewListId
    {
        get => _newListId;
        set
        {
            if (!CreateFlag) return;
            _newListId = value;
            UserList ul = new()
            {
                Id = value
            };
            ul = _uls.GetUserListById(ul);
            UserLists.Add(ul);
            CreateFlag = false;
        }
    }

    public UserListViewModel(UserListService userListService, ItemService itemService)
    {
        _uls = userListService;
        _itemService = itemService;
        Title = "Мои цели";
    }

    [RelayCommand]
    public void GetUserLists()
    {
        if (IsBusy)
            return;

        try
        {
            IsBusy = true;
            var userLists = _uls.GetUserLists();

            if (UserLists.Count != 0)
                UserLists.Clear();

            foreach (var userList in userLists)
            {
                UserLists.Add(userList);
            }

        }
        catch (Exception e)
        {
            Debug.WriteLine(e);
            Shell.Current.DisplayAlert("Error!",
                $"Unable to get UserLists: {e.Message}", "Ok");
        }
        finally
        {
            IsBusy = false;
        }
    }
    [RelayCommand]
    public async void CreateUserList()
    {
        if (IsBusy)
            return;


        await Shell.Current.GoToAsync($"{nameof(UserListDataInput)}", true,
            new Dictionary<string, object>
            {
                {"UserLists", UserLists}
            });

    }

    [RelayCommand]
    public async void GoToListItems(UserList ul)
    {
        if (IsBusy || ul is null)
            return;
        try
        {
            ul.Goals = _itemService.GetItemByParentId(ul);
        }
        catch (Exception e)
        {
            ul.Goals.Clear();
        }

        await Shell.Current.GoToAsync($"{nameof(UserListDetails)}?id={ul.Id}", true,
            new Dictionary<string, object>
            {
                {"UserList", ul}
            });
    }

    [RelayCommand]
    public void DeleteItem(UserList ul)
    {
        _uls.ArchiveUserList(ul);

        UserLists.Remove(ul);

    }

    [RelayCommand]
    public void CountPercentage()
    {
        foreach (var userlist in UserLists)
        {
            if (_itemService.GetUserListItems(userlist).Count > 0)
            {
                var completedCount = _itemService.GetUserListItems(userlist).Count(item => item.IsCompleted);
                double completedPercentage = Math.Round((double)completedCount / _itemService.GetUserListItems(userlist).Count * 100);
                userlist.Percentage = completedPercentage;
                if (completedPercentage <= 33) userlist.Color = Color.FromArgb("fdd4a6");
                else if (completedPercentage is > 33 and <= 67) userlist.Color = Color.FromArgb("ffff95");
                else if (completedPercentage > 67) userlist.Color = Color.FromArgb("99ff99");
            }
            else
            {
                userlist.Percentage = 0;
                userlist.Color = Color.FromArgb("#fdd4a6");
            }
        }
    }

    [RelayCommand]
    public async void ChangeNameDialog(UserList ul)
    {
        if (IsBusy)
            return;

        IsBusy = true;

        var result = await Shell.Current.DisplayPromptAsync("Изменить название", "Введите новое название:");

        if (result is not null)
        {
           ul.Name = result;
            _uls.UnarchiveUserList(ul);
            RefreshUserListScreen();
        }
        IsBusy = false;
    }

    [RelayCommand]
    public void RefreshUserListScreen()
    {

        IsRefreshing = true;

        UserLists.Clear();

        foreach (var item in _uls.GetUserLists())
        {
            UserLists.Add(item);
        }
        IsRefreshing = false;
    }
}
