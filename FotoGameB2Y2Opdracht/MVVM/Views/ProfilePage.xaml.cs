namespace FotoGameB2Y2Opdracht.MVVM.Views;

public partial class ProfilePage : ContentPage
{
	public ProfilePage()
	{
		InitializeComponent();
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
}