namespace BucketListMAUI;

public partial class MainPage : ContentPage
{
	int count = 0;

	public MainPage()
	{
		InitializeComponent();
		CounterBtn.Clicked += OnCounterClicked;
	}

	private void OnCounterClicked(object sender, EventArgs e)
	{
		count++;

		if (count == 69) { CounterLabel.Text = "Nice"; }
		if (count == 1)
		{
			CounterBtn.Text = $"Clicked {count} time";
            CounterLabel.Text = "Not funny";
        }
		else
            CounterLabel.Text = "Not funny";
        CounterBtn.Text = $"Clicked {count} times";

		SemanticScreenReader.Announce(CounterBtn.Text);
	}
}

