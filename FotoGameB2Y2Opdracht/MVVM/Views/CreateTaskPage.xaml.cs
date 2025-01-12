using FotoGameB2Y2Opdracht.MVVM.Models;
using FotoGameB2Y2Opdracht.MVVM.ViewModels;
using FotoGameB2Y2Opdracht.Services;

namespace FotoGameB2Y2Opdracht.MVVM.Views;

public partial class CreateTaskPage : ContentPage
{
    private readonly TasksViewModel _tasksViewModel;

    public CreateTaskPage(TasksViewModel tasksViewModel)
    {
        InitializeComponent();
        _tasksViewModel = tasksViewModel;
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
        string title = TitleEntry.Text;
        string description = DescriptionEditor.Text;

        if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(description))
        {
            await DisplayAlert("Error", "Please fill in all fields.", "OK");
            return;
        }

        var task = new Tasks
        {
            Title = title,
            Description = description
        };

        await _tasksViewModel.AddTask(task);

        await Shell.Current.GoToAsync("///TasksPage");
    }
}
