using FotoGameB2Y2Opdracht.MVVM.Models;
using FotoGameB2Y2Opdracht.MVVM.ViewModels;
using FotoGameB2Y2Opdracht.Services;

namespace FotoGameB2Y2Opdracht.MVVM.Views
{
    public partial class TasksPage : ContentPage
    {
        private readonly LocalDbService _dbService;

        public TasksPage(LocalDbService dbService, TasksViewModel viewModel)
        {
            InitializeComponent();
            _dbService = dbService;
            BindingContext = viewModel;
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

        // Navigeren naar de detailpagina van een taak
        private async void OnViewTaskClicked(object sender, EventArgs e)
        {
            var taskId = (int)((Button)sender).CommandParameter;
            await Shell.Current.GoToAsync($"///TaskPage?taskId={taskId}");
        }

        // Navigeren naar de profielpagina
        private async void OnProfileTapped(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("///ProfilePage");
        }

        // Navigeren naar de winkelpagina
        private async void OnShopTapped(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("///ShopPage");
        }

        // Navigeren naar de hoofdpagina
        private async void OnMainTapped(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("///MainPage");
        }

        // Navigeren naar de opdrachtenpagina (huidige pagina)
        private async void OnTasksTapped(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("///TasksPage");
        }

        // Navigeren naar de geclaimde opdrachtenpagina
        private async void OnClaimsTapped(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("///ClaimsPage");
        }

        // Navigeren naar de pagina voor het aanmaken van een nieuwe taak
        private async void OnCreateTaskButtonClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("///CreateTaskPage");
        }
    }
}
