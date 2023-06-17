﻿using BucketListMAUI.View;
using System.Timers;

//ViewModel для страницы с задачами

namespace BucketListMAUI.ViewModel;
[QueryProperty("UserList", "UserList")]
public partial class UserListDetailViewModel: BaseViewModel
{
    private readonly ItemService _itemService;

    System.Timers.Timer undoTimer;
    Stack<Item> undoItemBuffer;

    public UserListDetailViewModel(ItemService itemService)
    {
        _itemService = itemService;
        undoItemBuffer = new Stack<Item>();

        HasUndo = false;

        OnPropertyChanged(nameof(HasUndo));
        undoTimer = new System.Timers.Timer(5000);
        undoTimer.Elapsed += new ElapsedEventHandler(UndoTimerTick);
    }

    UserList userList;

    [ObservableProperty]
    string newItemName;


    public UserList UserList
    {
        get => userList;
        set
        {
            userList = value;

            Title = $"{UserList.Name}";

            OnUserListChanged(value);
            OnPropertyChanged(nameof(UserList));
            OnPropertyChanged(nameof(Title));
            OnPropertyChanged(nameof(UserList.Items));
        }
    }

    [ObservableProperty]
    public bool isRefreshing;

    [ObservableProperty]
    public bool hasUndo = false;


    [RelayCommand]
    public async void GoBackToListScreen()
    {
        await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
        //await Shell.Current.GoToAsync($"..");
    }

    public void OnUserListChanged(UserList value)
    {
        
    }

    [RelayCommand]
    public void RefreshUserListDetailScreen()
    {

        IsRefreshing = true;
        UserList.Items.Clear();

        
        UserList.Items = _itemService.GetUserListItems(UserList);

        UserListNotifers();

        IsRefreshing = false;
    }

    [RelayCommand]
    public async void GoToItemDetail(Item item)
    {
        await Shell.Current.DisplayAlert(item.Name, $"CreationDate: {item.CreationDate}", "Ok");
    }

    [RelayCommand]
    public void ItemWasChecked(Item item)
    {
        _itemService.UpdateItem(item);

        UserListNotifers();

    }


    [RelayCommand]
    public async void NewItemDialog()
    {
        if (IsBusy)
            return;

        IsBusy = true;

        string result = await Shell.Current.DisplayPromptAsync("Новая задача", "Введите новую задачу:");

        if (result is not null)
        {
            OnItemEntryCompleted(result);
            RefreshUserListDetailScreen();
        }
        IsBusy = false;
    }

    [RelayCommand]
    public async void OnItemEntryCompleted(string itemName)
    {


        try
        {
            var item = new Item(itemName);

            item.ParentId = UserList.Id;

            var newItem = new Item(item);

            newItem = _itemService.CreateItem(newItem);
            UserList.Items.Add(newItem);

            UserListNotifers();


        }
        catch (FeatureNotEnabledException e) //Возможно, это неправильное использование данного типа исключения, но вроде как подходит
        {
            

        }
    }

    [RelayCommand]
    public void DeleteItem(Item item)
    {

        _itemService.DeleteItem(item);
        UserList.Items.Remove(item);

        UserListNotifers();
    }

    [RelayCommand]
    public async void ChangeItemNameDialog(Item item)
    {
        if (IsBusy)
            return;

        IsBusy = true;

        string result = await Shell.Current.DisplayPromptAsync("Измeнить название", "Введите новое название:");

        if (result is not null)
        {
           item.Name = result;
            _itemService.UpdateItem(item);
           RefreshUserListDetailScreen();
        }
        IsBusy = false;
    }

    private void UserListNotifers()
    {
        Title = $"{UserList.Name}";

        OnUserListChanged(UserList);
        OnPropertyChanged(nameof(UserList));
        OnPropertyChanged(nameof(UserList.Items));
    }

    private void UndoTimerTick(object sender, EventArgs e)
    {
        HasUndo = false;
        undoTimer.Stop();

    }

}
