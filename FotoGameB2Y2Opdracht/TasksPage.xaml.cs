namespace FotoGameB2Y2Opdracht;

public partial class TasksPage : ContentPage
{
    public TasksPage()
    {
        InitializeComponent();
    }

    private async void OnViewTaskClicked(object sender, EventArgs e)
    {
        await DisplayAlert("View Task", "Navigating to the full task details.", "OK");
    }

    // Navigeren naar de profielpagina
    private async void OnProfileTapped(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("ProfilePage");
    }

    // Navigeren naar de winkelpagina
    private async void OnShopTapped(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("ShopPage");
    }

    // Navigeren naar de hoofdpagina
    private async void OnMainTapped(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///MainPage");
    }

    // Navigeren naar de opdrachtenpagina
    private async void OnTasksTapped(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///TasksPage");
    }

    // Navigeren naar de geclaimde opdrachtenpagina
    private async void OnClaimsTapped(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("ClaimsPage");
    }
}
