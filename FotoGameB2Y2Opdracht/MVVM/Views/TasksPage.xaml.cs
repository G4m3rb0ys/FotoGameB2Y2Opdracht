using FotoGameB2Y2Opdracht.MVVM.Models;
using FotoGameB2Y2Opdracht.Services;

namespace FotoGameB2Y2Opdracht.MVVM.Views;

public partial class TasksPage : ContentPage
{
    private readonly LocalDbService _dbService;

    public TasksPage(LocalDbService dbService)
    {
        InitializeComponent();
        _dbService = dbService;
        LoadTasks();
    }

    private async void LoadTasks()
    {
        var tasks = await _dbService.GetTasks();

        if (tasks.Any())
        {
            TasksCollectionView.ItemsSource = tasks.OrderBy(t => t.Deadline).ToList();
            TasksCollectionView.IsVisible = true;
            NoTasksLabel.IsVisible = false;
        }
        else
        {
            TasksCollectionView.IsVisible = false;
            NoTasksLabel.IsVisible = true;
        }
    }

    private async void OnViewTaskClicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.CommandParameter is int taskId)
        {
            await Shell.Current.GoToAsync($"///TaskPage?taskId={taskId}");
        }
    }

    private async void OnCreateTaskButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///CreateTaskPage");
    }

    // Navigatie naar verschillende pagina's
    private async void OnProfileTapped(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///ProfilePage");
    }

    private async void OnShopTapped(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///ShopPage");
    }

    private async void OnMainTapped(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///MainPage");
    }

    private async void OnTasksTapped(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///TasksPage");
    }

    private async void OnClaimsTapped(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///ClaimsPage");
    }
}
