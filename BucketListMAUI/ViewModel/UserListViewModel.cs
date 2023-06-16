using System.Collections.ObjectModel;
using System.Diagnostics;
using BucketListMAUI.View;

namespace BucketListMAUI.ViewModel;

[QueryProperty("CreateFlag", "createflag")]
[QueryProperty("NewListId", "id")]
public partial class UserListViewModel : BaseViewModel
{
    private UserListService _uls;
    private ItemService _itemService;
    private int _newListId;

    [ObservableProperty]
    public bool isRefreshing;

    public ObservableCollection<UserList> UserLists { get; } = new();

    public bool CreateFlag { get; set; } = false;

    //TODO there is probably a better way to do this? idk
    public int NewListId
    {
        get { return _newListId; }
        set
        {
            if (CreateFlag)
            {
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
    }

    public UserListViewModel(UserListService userListService, ItemService itemService)
    {
        _uls = userListService;
        _itemService = itemService;
        Title = "My Goals";
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
            ul.Items = _itemService.GetItemByParentId(ul);
        }
        catch (Exception e)
        {
            ul.Items.Clear();
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
        int completedCount = _itemService.GetUserListItems(UserLists[0]).Count(item => item.IsCompleted);
        int completedPercentage = completedCount / _itemService.GetUserListItems(UserLists[0]).Count * 100;
        UserLists[0].Percentage = completedPercentage;
    }
    [RelayCommand]
    public void UpdatePrimaryColorPressed(string themeName)
    {
        Preferences.Set("Theme", themeName);

        ICollection<ResourceDictionary> mergedDictionaries = Application.Current.Resources.MergedDictionaries;
        if (mergedDictionaries != null)
        {
            foreach (ResourceDictionary dictionaries in mergedDictionaries)
            {
                var primaryFound = dictionaries.TryGetValue(themeName + "Primary", out var primary);
                if (primaryFound)
                    dictionaries["Primary"] = primary;

                var secondaryFound = dictionaries.TryGetValue(themeName + "Secondary", out var secondary);
                if (secondaryFound)
                    dictionaries["Secondary"] = secondary;

                var tertiaryFound = dictionaries.TryGetValue(themeName + "Tertiary", out var tertiary);
                if (tertiaryFound)
                    dictionaries["Tertiary"] = tertiary;

                var accentFound = dictionaries.TryGetValue(themeName + "Accent", out var accent);
                if (accentFound)
                    dictionaries["Accent"] = accent;

                var darkAccentFound = dictionaries.TryGetValue(themeName + "DarkAccent", out var darkAccent);
                if (darkAccentFound)
                    dictionaries["DarkAccent"] = darkAccent;


            }
        }

    }
}
