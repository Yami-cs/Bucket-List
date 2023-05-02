using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BucketListMAUI
{
    public partial class NewTaskViewModel: ObservableObject
    {
        public NewTaskViewModel() { }
       
        [ObservableProperty]
        string text;

        [ObservableProperty]
        string description;

        [RelayCommand]
        async void Save()
        {
            Task newItem = new Task()
            {
                Id = Guid.NewGuid().ToString(),
                Text = Text,
                Title = Description
            };

            //await DataStore.AddItemAsync(newItem);

            await Shell.Current.GoToAsync("..");
        }

        [RelayCommand]
        async void Cancel()
        {
            await Shell.Current.GoToAsync("..");
        }

    }
}
