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
        var task = await _dbService.GetTaskById(TaskId);
        if (task == null)
        {
            await DisplayAlert("Error", "Task not found.", "OK");
            return;
        }

        var claim = new Claim
        {
            TaskId = task.Id,
            UserId = _userService.GetCurrentUser().Id,
            Title = task.Title,
            Description = task.Description,
            Deadline = task.Deadline,
            Points = task.Cost,
            IsWeekly = task.IsWeekly,
        };

        await _dbService.AddClaim(claim);

        if (task.IsWeekly)
        {
            var weeklyGoals = task.WeeklyGoals.Select(goal => new ClaimWeeklyGoal
            {
                WeekNumber = goal.Weeknumber,
                Description = goal.Description,
                IsCompleted = false
            }).ToList();

            await _dbService.AddWeeklyGoalsToClaim(claim.Id, weeklyGoals);
        }

        await DisplayAlert("Task Accepted", "You have claimed the task!", "OK");
        await Shell.Current.GoToAsync("///ClaimsPage");
    }


    private async void OnGoBackClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///TasksPage");
    }
}
