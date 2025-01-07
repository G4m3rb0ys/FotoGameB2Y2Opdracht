namespace FotoGameB2Y2Opdracht.MVVM.Views;

public partial class ForgotPasswordPage : ContentPage
{
    public ForgotPasswordPage()
    {
        InitializeComponent();
    }

    private void OnRequestPasswordClicked(object sender, EventArgs e)
    {
        string email = EmailEntry.Text;

        if (string.IsNullOrWhiteSpace(email))
        {
            DisplayAlert("Error", "Please enter a valid email address.", "OK");
            return;
        }

        // reset melding tonen
        MessageLabel.Text = $"Password reset requested for {email}.";
        MessageLabel.IsVisible = true;
    }

    private async void OnBackButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///LoginPage");
    }
}