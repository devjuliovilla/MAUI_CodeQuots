using System;

namespace CodeQuotes;

public partial class MainPage : ContentPage
{
	List<string> quotes = new List<string>();

	public MainPage()
	{
		InitializeComponent();
	}

	protected override async void OnAppearing()
	{
		base.OnAppearing();
        await LoadMauiAsset();
    }

    private async Task LoadMauiAsset()
    {
        using var stream = await FileSystem.OpenAppPackageFileAsync("quotes.txt");
        using var reader = new StreamReader(stream);

        while (reader.Peek() != -1)
        {
            quotes.Add(reader.ReadLine());
        }
    }

    Random rand = new Random();
    private void btn_randomQuote_Clicked(object sender, EventArgs e)
    {
        var startColor= System.Drawing.Color.FromArgb(
            rand.Next(0, 256),
            rand.Next(0, 256),
            rand.Next(0, 256));

        var endColor = System.Drawing.Color.FromArgb(
            rand.Next(0, 256),
            rand.Next(0, 256),
            rand.Next(0, 256));

        var colors = ColorUtility.ColorControls.GetColorGradient(
            startColor, endColor, 6);

        float stopOffset = 0f;
        var stops = new GradientStopCollection();
        foreach (var color in colors)
        {
            stops.Add(new GradientStop(Color.FromArgb(color.Name), stopOffset));
            stopOffset += .2f;
        }
        var gradient = new RadialGradientBrush(stops);
        background.Background = gradient;

        int index = rand.Next(quotes.Count);
        label_quote.Text = quotes[index];
    }
}

