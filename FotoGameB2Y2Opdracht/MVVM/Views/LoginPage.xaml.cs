namespace FotoGameB2Y2Opdracht.MVVM.Views;

    public partial class LoginPage : ContentPage
    {
        private const string HardcodedUsername = "admin";
        private const string HardcodedPassword = "password123";

        public LoginPage()
        {
            InitializeComponent();
        }

        private async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            var username = UsernameEntry.Text;
            var password = PasswordEntry.Text;

            //ErrorMessageLabel.IsVisible = false;
            //await Shell.Current.GoToAsync("///MainPage");
        if (username == HardcodedUsername && password == HardcodedPassword)
            {
                ErrorMessageLabel.IsVisible = false;
                await Shell.Current.GoToAsync("///MainPage");
        }
            else
            {
                ErrorMessageLabel.Text = "Invalid username or password.";
                ErrorMessageLabel.IsVisible = true;
            }
        }
    // navigatie naar registerpagina
    private async void OnRegisterButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///RegisterPage");
    }

    // navigatie naar wachtwoord vergeten pagina
    private async void OnForgotPasswordClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///ForgotPasswordPage");
    }
}