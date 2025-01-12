using FotoGameB2Y2Opdracht.MVVM.Models;
using FotoGameB2Y2Opdracht.Services;
using Microsoft.Maui.Storage;

namespace FotoGameB2Y2Opdracht.MVVM.Views;

[QueryProperty(nameof(ClaimId), "claimId")]
public partial class ClaimDetailsPage : ContentPage
{
    private readonly LocalDbService _dbService;
    private int _claimId;

    public int ClaimId
    {
        get => _claimId;
        set
        {
            _claimId = value;
            LoadClaimDetailsAsync();
        }
    }

    public ClaimDetailsPage(LocalDbService dbService)
    {
        InitializeComponent();
        _dbService = dbService;
    }

    private async void LoadClaimDetailsAsync()
    {
        var claim = await _dbService.GetClaimByIdAsync(ClaimId);
        if (claim != null)
        {
            TitleLabel.Text = claim.Title;
            DescriptionLabel.Text = claim.Description;
            DeadlineLabel.Text = $"Deadline: {claim.Deadline:yyyy-MM-dd}";
            PointsLabel.Text = $"Points: {claim.Points}";

            var weeklyGoals = await _dbService.GetWeeklyGoalsForClaim(claim.Id);
            if (weeklyGoals.Any())
            {
                WeeklyGoalsLayout.IsVisible = true;
                WeeklyGoalsCollectionView.ItemsSource = weeklyGoals;
            }
        }
        else
        {
            await DisplayAlert("Error", "Claim not found.", "OK");
            await Shell.Current.GoToAsync("///ClaimsPage");
        }
    }

    private async void OnBackButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///ClaimsPage");
    }

    private async void OnAddPhotoClicked(object sender, EventArgs e)
    {
        try
        {
            await CheckAndRequestPermissionsAsync();

            var photo = await MediaPicker.PickPhotoAsync(new MediaPickerOptions { Title = "Select a Photo" });
            if (photo != null)
            {
                var stream = await photo.OpenReadAsync();
                SelectedImage.Source = ImageSource.FromStream(() => stream);
                SelectedImage.IsVisible = true;

                // Foto toevoegen aan de database
                await _dbService.AddPhotoToClaimAsync(ClaimId, photo.FullPath);
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Unable to pick photo: {ex.Message}", "OK");
        }
    }

    private async Task CheckAndRequestPermissionsAsync()
    {
        var status = await Permissions.CheckStatusAsync<Permissions.StorageRead>();
        if (status != PermissionStatus.Granted)
        {
            status = await Permissions.RequestAsync<Permissions.StorageRead>();
        }

        if (status != PermissionStatus.Granted)
        {
            await DisplayAlert("Permission Required", "We need access to your photos.", "OK");
        }
    }
}
