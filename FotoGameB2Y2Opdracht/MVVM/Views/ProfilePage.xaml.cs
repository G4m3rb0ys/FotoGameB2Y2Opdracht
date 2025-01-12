using FotoGameB2Y2Opdracht.MVVM.Models;
using FotoGameB2Y2Opdracht.Services;
using Microsoft.Maui.Storage;

namespace FotoGameB2Y2Opdracht.MVVM.Views;

public partial class ProfilePage : ContentPage
{
    private readonly UserService _userService;
    private readonly LocalDbService _dbService;
    private User _currentUser;

    public ProfilePage(UserService userService, LocalDbService dbService)
    {
        InitializeComponent();
        _userService = userService;
        _dbService = dbService;

        LoadUserData();
    }

    private void LoadUserData()
    {
        _currentUser = _userService.GetCurrentUser();
        if (_currentUser != null)
        {
            UsernameLabel.Text = _currentUser.Username;
            if (!string.IsNullOrEmpty(_currentUser.ProfilePhotoUrl))
            {
                ProfileImage.Source = ImageSource.FromFile(_currentUser.ProfilePhotoUrl);
            }
        }
    }

    // Username wijzigen
    private async void OnEditUsernameClicked(object sender, TappedEventArgs e)
    {
        string newUsername = await DisplayPromptAsync("Change Username", "Enter new username:", initialValue: UsernameLabel.Text);

        if (!string.IsNullOrWhiteSpace(newUsername))
        {
            UsernameLabel.Text = newUsername;
            _currentUser.Username = newUsername;

            await _dbService.UpdateUser(_currentUser);
        }
    }

    // Profielfoto wijzigen
    private async void OnChangeProfilePhotoClicked(object sender, TappedEventArgs e)
    {
        string action = await DisplayActionSheet("Choose an option", "Cancel", null, "Take Photo", "Pick from Gallery");

        try
        {
            FileResult photo = null;

            if (action == "Take Photo")
            {
                photo = await MediaPicker.CapturePhotoAsync();
            }
            else if (action == "Pick from Gallery")
            {
                photo = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
                {
                    Title = "Select Profile Photo"
                });
            }

            if (photo != null)
            {
                var stream = await photo.OpenReadAsync();
                ProfileImage.Source = ImageSource.FromStream(() => stream);

                // Sla de foto-URL op in de gebruiker
                _currentUser.ProfilePhotoUrl = photo.FullPath;
                await _dbService.UpdateUser(_currentUser);
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Unable to pick or take photo: {ex.Message}", "OK");
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
}
