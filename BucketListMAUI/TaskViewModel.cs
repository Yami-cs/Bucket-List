using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BucketListMAUI
{
    public partial class TaskViewModel: ObservableObject
    {
        public TaskViewModel() 
        {
            Items = new ObservableCollection<string>();        
        }
        [ObservableProperty]
        ObservableCollection<string> items;

        [ObservableProperty]
        string text;
     
        [RelayCommand]
        void Add()
        {
            if (string.IsNullOrWhiteSpace(Text))
                return;
            Items.Add(Text);
            Text = string.Empty;
        }
        [RelayCommand]
        void Delete(string s)
        {
            if(Items.Contains(s))
                Items.Remove(s);
        }
        [RelayCommand]
        async Task Tap(string s)
        {
            await Shell.Current.GoToAsync($"{nameof(DetailPage)}?Text={s}");
        }
    }
}
