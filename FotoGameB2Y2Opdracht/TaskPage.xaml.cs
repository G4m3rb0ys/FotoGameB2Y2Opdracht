namespace FotoGameB2Y2Opdracht;

public partial class TaskPage : ContentPage
{
    public TaskPage()
    {
        InitializeComponent();
    }

    private async void OnAcceptTaskClicked(object sender, EventArgs e)
    {
        await DisplayAlert("Task Accepted", "You have accepted Task 1!", "OK");
    }

    private async void OnGoBackClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///TasksPage");
    }
}