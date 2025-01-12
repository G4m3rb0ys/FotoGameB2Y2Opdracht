using FotoGameB2Y2Opdracht.Services;

namespace FotoGameB2Y2Opdracht.MVVM.Views
{
    public partial class LoginPage : ContentPage
    {
        private readonly UserService _userService;

        public LoginPage(UserService userService)
        {
            InitializeComponent();
            _userService = userService;

            if (_userService.IsLoggedIn())
            {
                Shell.Current.GoToAsync("///MainPage");
            }
        }

        private async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            var username = UsernameEntry.Text;
            var password = PasswordEntry.Text;

            var user = await _userService.LoginAsync(username, password);

            if (user != null)
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

        private async void OnRegisterButtonClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("///RegisterPage");
        }

        private async void OnForgotPasswordClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("///ForgotPasswordPage");
        }

        private async void OnSkipButtonClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("///MainPage");
        }
    }
}
