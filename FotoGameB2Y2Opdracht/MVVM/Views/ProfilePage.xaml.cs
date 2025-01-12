using FotoGameB2Y2Opdracht.Services;

namespace FotoGameB2Y2Opdracht.MVVM.Views;

public partial class ProfilePage : ContentPage
{
	public ProfilePage()
	{
		InitializeComponent();
	}

    private readonly UserService _userService;

    public ProfilePage(UserService userService)
    {
        InitializeComponent();
        _userService = userService;

        var currentUser = _userService.GetCurrentUser();
        if (currentUser != null)
        {
            UsernameLabel.Text = currentUser.Username;
        }
    }

    // Username wijzigen
    private async void OnEditUsernameClicked(object sender, TappedEventArgs e)
    {
        string newUsername = await DisplayPromptAsync("Change Username", "Enter new username:", initialValue: UsernameLabel.Text);

        if (!string.IsNullOrWhiteSpace(newUsername))
        {
            UsernameLabel.Text = newUsername;
        }
    }

    // Profielfoto wijzigen
    private async void OnChangeProfilePhotoClicked(object sender, TappedEventArgs e)
    {
        try
        {
            var photo = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
            {
                Title = "Select Profile Photo"
            });

            if (photo != null)
            {
                var stream = await photo.OpenReadAsync();
                ProfileImage.Source = ImageSource.FromStream(() => stream);
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Unable to pick photo: {ex.Message}", "OK");
        }
    }

    // Uitloggen
    private async void OnLogoutButtonClicked(object sender, EventArgs e)
    {
        _userService.Logout();
        await Shell.Current.GoToAsync("///LoginPage");
    }

    // Navigatie naar profielpagina
    private async void OnProfileTapped(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync("//ProfilePage");
    }

    // Navigatie naar de winkelpagina
    private async void OnShopTapped(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync("//ShopPage");
    }

    // Navigatie naar de hoofdpagina
    private async void OnMainTapped(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync("//MainPage");
    }

    // Navigatie naar de takenpagina
    private async void OnTasksTapped(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync("//TasksPage");
    }

    // Navigatie naar de claims-pagina
    private async void OnClaimsTapped(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync("//ClaimsPage");
    }

    private void Button_Clicked(object sender, EventArgs e)
    {

    }
}