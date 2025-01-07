namespace FotoGameB2Y2Opdracht.MVVM.Views;

public partial class RegisterPage : ContentPage
{
    public RegisterPage()
    {
        InitializeComponent();
    }

    private void OnRegisterButtonClicked(object sender, EventArgs e)
    {
        string ageText = AgeEntry.Text;
        string email = EmailEntry.Text;
        string password = PasswordEntry.Text;

        if (string.IsNullOrWhiteSpace(ageText) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
        {
            DisplayAlert("Error", "All fields are required!", "OK");
            return;
        }

        if (!int.TryParse(ageText, out int age) || age < 18)
        {
            DisplayAlert("Error", "Age must be a number and at least 18.", "OK");
            return;
        }

        if (!email.Contains("@") || !email.Contains("."))
        {
            DisplayAlert("Error", "Invalid email format.", "OK");
            return;
        }


        // TIJDELIJK toont aanmaak
        MessageLabel.Text = $"Account created for {email}!";
        MessageLabel.IsVisible = true;

    }

    //navigate naar login
    private async void OnBackButtonClicked(object sender, EventArgs e)
    {
    await Shell.Current.GoToAsync("///LoginPage");
    }
}