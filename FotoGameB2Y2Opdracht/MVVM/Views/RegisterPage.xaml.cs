namespace FotoGameB2Y2Opdracht.MVVM.Views;
using FotoGameB2Y2Opdracht.MVVM.Models;
using FotoGameB2Y2Opdracht.Services;

public partial class RegisterPage : ContentPage

{
    private readonly LocalDbService _dbService;

    public RegisterPage(LocalDbService dbService)
    {
        InitializeComponent();
        _dbService = dbService;
    }

    private async void OnRegisterButtonClicked(object sender, EventArgs e)
    {
        string username = UsernameEntry.Text;
        string email = EmailEntry.Text;
        string password = PasswordEntry.Text;

        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
        {
            await DisplayAlert("Error", "All fields are required!", "OK");
            return;
        }

        if (!email.Contains("@") || !email.Contains("."))
        {
            await DisplayAlert("Error", "Invalid email format.", "OK");
            return;
        }

        if (password.Length < 6)
        {
            await DisplayAlert("Error", "Password must be at least 6 characters long.", "OK");
            return;
        }

        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
        {
            await DisplayAlert("Error", "Please fill in all fields.", "OK");
            return;
        }

        // Controleer of de gebruiker al bestaat
       // var (usernameExists, emailExists) = await _dbService.UserExists(username, email);

        //if (usernameExists && emailExists)
        //{
            //await DisplayAlert("Error", "A user with this username and email already exists.", "OK");
            //return;
        //}

        //if (usernameExists)
        //{
            //await DisplayAlert("Error", "A user with this username already exists.", "OK");
            //return;
        //}

        //if (emailExists)
        //{
            //await DisplayAlert("Error", "A user with this email already exists.", "OK");
            //return;
        //}

        var newUser = new User
        {
            Username = username,
            Email = email,
            Password = password
        };

        await _dbService.CreateUser(newUser); 
        await DisplayAlert("Success", "User registered successfully!", "OK");


    }


    //navigate naar login
    private async void OnBackButtonClicked(object sender, EventArgs e)
    {
        string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "app.db");
        Console.WriteLine($"Database Path: {dbPath}");
        await DisplayAlert("Database Path", dbPath, "OK");
        await Shell.Current.GoToAsync("///LoginPage");
    }
}