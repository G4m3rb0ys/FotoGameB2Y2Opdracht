namespace FotoGameB2Y2Opdracht.MVVM.Views;
using FotoGameB2Y2Opdracht.MVVM.Models;
using FotoGameB2Y2Opdracht.Services;
using SQLite;

public partial class CreateTaskPage : ContentPage
{
    private readonly LocalDbService _dbService;
    private readonly UserService _userService;

    public CreateTaskPage(LocalDbService dbService, UserService userService)
    {
        InitializeComponent();
        _dbService = dbService;
        _userService = userService;
    }

    private void OnIsWeeklyCheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        bool isChecked = e.Value;
        NumberOfWeeksEntry.IsVisible = isChecked;
        WeeklyGoalsLayout.IsVisible = isChecked;

        if (!isChecked)
        {
            WeeklyGoalsLayout.Children.Clear();
        }
    }

    private void OnWeeksChanged(object sender, TextChangedEventArgs e)
    {
        WeeklyGoalsLayout.Children.Clear();

        if (int.TryParse(e.NewTextValue, out int weeks) && weeks > 0)
        {
            for (int i = 1; i <= weeks; i++)
            {
                WeeklyGoalsLayout.Children.Add(new Entry
                {
                    Placeholder = $"Enter goal for week {i}",
                    FontSize = 16
                });
            }
        }
    }

    private async void OnSaveTaskClicked(object sender, EventArgs e)
    {
        var currentUser = _userService.GetCurrentUser();
        string title = TitleEntry.Text;
        string description = DescriptionEditor.Text;
        DateTime deadline = DeadlinePicker.Date;
        bool isWeekly = IsWeeklyCheckBox.IsChecked;

        if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(description))
        {
            await DisplayAlert("Error", "Please fill in all fields.", "OK");
            return;
        }

        var task = new Tasks
        {
            Title = title,
            Description = description,
            Deadline = deadline,
            IsWeekly = isWeekly,
            CreatorID = currentUser.Id
        };

        await _dbService.CreateTask(task);

        if (isWeekly)
        {
            int currentWeek = 1;
            List<WeeklyTaskGoal> weeklyTasks = new List<WeeklyTaskGoal>();

            foreach (var child in WeeklyGoalsLayout.Children)
            {
                if (child is Entry entry && !string.IsNullOrWhiteSpace(entry.Text))
                {
                    weeklyTasks.Add(new WeeklyTaskGoal
                    {
                        TaskID = task.Id,
                        Weeknumber = currentWeek,
                        Description = entry.Text
                    });

                    currentWeek++;
                }
            }

            await _dbService.AddWeeklyGoals(weeklyTasks);
        }

        string message = isWeekly ? "Weekly task created!" : "Task created!";
        await DisplayAlert("Success", message, "OK");

        await Shell.Current.GoToAsync("///TasksPage");
    }
}
