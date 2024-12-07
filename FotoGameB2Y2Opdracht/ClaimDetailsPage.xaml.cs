using Microsoft.Maui.Storage;

namespace FotoGameB2Y2Opdracht;

public partial class ClaimDetailsPage : ContentPage
{
    public ClaimDetailsPage()
    {
        InitializeComponent();

        // Hardcoded gegevens
        TitleLabel.Text = "Weekly Task";
        DescriptionLabel.Text = "This is a weekly task with specific goals for each week.";
        DeadlineLabel.Text = "Deadline: 2023-12-31";
        PointsLabel.Text = "Points: 150";

        // Hardcoded wekelijkse doelen
        var weeklyGoals = new List<WeeklyGoal>
        {
            new WeeklyGoal { Goal = "Complete Week 1 goal", IsCurrentWeek = false },
            new WeeklyGoal { Goal = "Complete Week 2 goal", IsCurrentWeek = true },
            new WeeklyGoal { Goal = "Complete Week 3 goal", IsCurrentWeek = false }
        };

        WeeklyGoalsLayout.IsVisible = true; // Toon wekelijkse doelen
        WeeklyGoalsCollectionView.ItemsSource = weeklyGoals;
        SelectedImage.IsVisible = false;    // Verberg afbeelding standaard
    }

    private async void OnBackButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///ClaimsPage");
    }

    private async Task CheckAndRequestPermissionsAsync()
    {
        // Controleer toestemming voor opslag en camera
        var status = await Permissions.CheckStatusAsync<Permissions.StorageRead>();
        if (status != PermissionStatus.Granted)
        {
            status = await Permissions.RequestAsync<Permissions.StorageRead>();
        }

        if (status != PermissionStatus.Granted)
        {
            await DisplayAlert("Permission Required", "We need access to your photos or camera.", "OK");
        }
    }

    private async void OnTakePhotoClicked(object sender, EventArgs e)
    {
        try
        {
            // Vraag toestemming voor camera
            await CheckAndRequestPermissionsAsync();

            // Maak een foto met de camera
            var photo = await MediaPicker.CapturePhotoAsync();
            if (photo != null)
            {
                var stream = await photo.OpenReadAsync();
                SelectedImage.Source = ImageSource.FromStream(() => stream);
                SelectedImage.IsVisible = true;
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Unable to capture photo: {ex.Message}", "OK");
        }
    }

    private async void OnAddPhotoClicked(object sender, EventArgs e)
    {
        try
        {
            // Vraag toestemming voor toegang tot opslag
            await CheckAndRequestPermissionsAsync();

            // Kies een foto uit de galerij
            var photo = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
            {
                Title = "Select a Photo"
            });

            if (photo != null)
            {
                var stream = await photo.OpenReadAsync();
                SelectedImage.Source = ImageSource.FromStream(() => stream);
                SelectedImage.IsVisible = true;
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Unable to pick photo: {ex.Message}", "OK");
        }
    }
}

public class WeeklyGoal
{
    public string Goal { get; set; }
    public bool IsCurrentWeek { get; set; }
}
