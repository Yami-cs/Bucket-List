using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace BucketListMAUI;

public partial class TaskPage : ContentPage
{
    public ObservableCollection<Task> Notes { get; set; }
    public TaskPage(TaskViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
        Notes = new ObservableCollection<Task>();

        var notesJson = Preferences.Get("Notes", "");
        if (!string.IsNullOrEmpty(notesJson))
        {
            var savedNotes = JsonConvert.DeserializeObject<List<Task>>(notesJson);
            Notes.Clear();
            foreach (var note in savedNotes)
            {
                Notes.Add(note);
            }
        }
    }
   /* private async void AddNoteButton_Clicked(object sender, EventArgs e)
    {
        // Show the AddNotePage as a modal popup
        var addNotePage = new NewTaskPage();
        await Navigation.PushModalAsync(addNotePage);
    }*/
    /* private void NoteListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
     {
         // Show the selected note's text in the editor
         var selectedNote = e.SelectedItem as Task;
         if (selectedNote != null)
         {
             TextEditor.Text = selectedNote.Text;
         }
     }*/
}