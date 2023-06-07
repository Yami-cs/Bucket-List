using Android.OS;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Google.Crypto.Tink.Signature;

namespace BucketListMAUI
{
    [QueryProperty("Text", "Text")]
    public partial class DetailViewModel: ObservableObject
    {
        public DetailViewModel()
        {
            Items = new ObservableCollection<string>();
        }

        private List<string> selectedItems = new List<string>();
        [ObservableProperty]
        int count;

        [ObservableProperty]
        ObservableCollection<string> items;

        [ObservableProperty]
        string text;

        [ObservableProperty]
        string text1;

        [RelayCommand]
        void Add()
        {
            if (string.IsNullOrWhiteSpace(Text1))
                return;
            Items.Add(Text1);
            Text1 = string.Empty;
        }

        [RelayCommand]
        void Delete(string s)
        {
            if (Items.Contains(s))
                Items.Remove(s);
        }


    }
}
