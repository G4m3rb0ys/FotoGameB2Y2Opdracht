namespace FotoGameB2Y2Opdracht.MVVM.Views;

public partial class CreateTaskPage : ContentPage
{
    public CreateTaskPage()
    {
        InitializeComponent();
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
        DateTime deadline = DeadlinePicker.Date;
        bool isWeekly = IsWeeklyCheckBox.IsChecked;

        if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(description))
        {
            await DisplayAlert("Error", "Please fill in all fields.", "OK");
            return;
        }

        List<string> weeklyGoals = new List<string>();
        if (isWeekly)
        {
            foreach (var child in WeeklyGoalsLayout.Children)
            {
                if (child is Entry entry && !string.IsNullOrWhiteSpace(entry.Text))
                {
                    weeklyGoals.Add(entry.Text);
                }
            }
        }

        string message = $"Task '{title}' created with deadline {deadline:yyyy-MM-dd}.";
        if (isWeekly)
        {
            message += $"\nWeekly goals:\n- {string.Join("\n- ", weeklyGoals)}";
        }

        await DisplayAlert("Task Created", message, "OK");

        await Shell.Current.GoToAsync("///TasksPage");

    }
}
