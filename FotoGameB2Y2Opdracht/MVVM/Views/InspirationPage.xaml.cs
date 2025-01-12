using System.Net.Http.Json;
using System.Text.Json;

namespace FotoGameB2Y2Opdracht.MVVM.Views;

public partial class InspirationPage : ContentPage
{
    private readonly HttpClient _httpClient;

    public InspirationPage()
    {
        InitializeComponent();
        _httpClient = new HttpClient();
    }

    private async void OnGetInspirationClicked(object sender, EventArgs e)
    {
        var client = new HttpClient();

        try
        {
            var response = await client.GetStringAsync("https://api.truthordarebot.xyz/api/wyr");

            var jsonDoc = JsonDocument.Parse(response);
            var question = jsonDoc.RootElement.GetProperty("question").GetString();

            InspirationLabel.Text = question;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
        }
    }

    private async void OnGoBackClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///MainPage");
    }

    private async void OnProfileTapped(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///ProfilePage");
    }

    private async void OnShopTapped(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///ShopPage");
    }

    private async void OnMainTapped(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///MainPage");
    }

    private async void OnTasksTapped(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///TasksPage");
    }

    private async void OnClaimsTapped(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///ClaimsPage");
    }
}

public class Activity
{
    public string ActivityText { get; set; }
}
