using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace BucketListMAUI;

public partial class TaskPage : ContentPage
{
    public TaskPage(TaskViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
       
    }  
}