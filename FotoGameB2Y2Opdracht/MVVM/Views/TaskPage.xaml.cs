using FotoGameB2Y2Opdracht.Services;
using FotoGameB2Y2Opdracht.MVVM.Models;

namespace FotoGameB2Y2Opdracht.MVVM.Views;

[QueryProperty(nameof(TaskId), "taskId")]
public partial class TaskPage : ContentPage
{
    private readonly LocalDbService _dbService;
    private readonly UserService _userService;

    public int TaskId { get; set; }

    public TaskPage(LocalDbService dbService, UserService userService)
    {
        InitializeComponent();
        _dbService = dbService;
        _userService = userService;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        var task = await _dbService.GetTaskById(TaskId);
        if (task != null)
        {
            TitleLabel.Text = task.Title;
            DescriptionLabel.Text = task.Description;
            DeadlineLabel.Text = $"Deadline: {task.Deadline:yyyy-MM-dd}";
            PointsLabel.Text = $"Points: {task.Cost}";
        }
        else
        {
            await DisplayAlert("Error", "Task not found.", "OK");
            await Shell.Current.GoToAsync("///TasksPage");
        }
    }

    private async void OnAcceptTaskClicked(object sender, EventArgs e)
    {
        var currentUser = _userService.GetCurrentUser();
        if (currentUser == null)
        {
            await DisplayAlert("Error", "No user is logged in.", "OK");
            return;
        }

        var existingClaim = await _dbService.GetClaimByUserAndTask(currentUser.Id, TaskId);
        if (existingClaim != null)
        {
            await DisplayAlert("Info", "You have already claimed this task.", "OK");
            return;
        }

        var claim = new Claim
        {
            TaskId = TaskId,
            UserId = currentUser.Id,
            Title = TitleLabel.Text,
            Description = DescriptionLabel.Text,
            Deadline = DateTime.Parse(DeadlineLabel.Text.Replace("Deadline: ", "")),
            Points = int.Parse(PointsLabel.Text.Replace("Points: ", "")),
            IsWeekly = false 
        };

        await _dbService.AddClaim(claim);

        await DisplayAlert("Success", $"You have claimed '{TitleLabel.Text}'!", "OK");
        await Shell.Current.GoToAsync("///ClaimsPage");
    }

    private async void OnGoBackClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///TasksPage");
    }
}
