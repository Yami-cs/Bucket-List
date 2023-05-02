using Newtonsoft.Json;

namespace BucketListMAUI;

public partial class NewTaskPage : ContentPage
{
    public string TaskTitle { get; set; }
    public string TaskText { get; set; }
    public Task Item { get; set; }
    public NewTaskPage(NewTaskViewModel vm)
    {

        InitializeComponent();
        BindingContext = vm;
    }
    /*private async void SaveButton_Clicked(object sender, EventArgs e)
    {
        // Create a new Note object with the entered title and text
        var newNote = new Task
        {
            Title = TaskTitle,
            Text = TaskText
        };
        (BindingContext as TaskPage)?.Notes?.Add(newNote);
        var notesJson = JsonConvert.SerializeObject((BindingContext as TaskPage)?.Notes);
        Preferences.Set("Notes", notesJson);

        // Close the modal popup
        await Navigation.PopModalAsync();
    }
    private async void CancelButton_Clicked(object sender, EventArgs e)
    {
        // Close the modal popup without saving the new note
        await Navigation.PopModalAsync();
    }*/
}