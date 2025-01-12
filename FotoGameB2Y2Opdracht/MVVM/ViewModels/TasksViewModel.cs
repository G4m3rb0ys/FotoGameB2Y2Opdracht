using System.Collections.ObjectModel;
using System.Windows.Input;
using FotoGameB2Y2Opdracht.MVVM.Models;
using FotoGameB2Y2Opdracht.Services;

namespace FotoGameB2Y2Opdracht.MVVM.ViewModels
{
    public class TasksViewModel : BaseViewModel
    {
        private readonly LocalDbService _dbService;

        public ObservableCollection<Tasks> TaskList { get; set; } = new();

        public ICommand ViewTaskCommand { get; }

        public TasksViewModel(LocalDbService dbService)
        {
            _dbService = dbService;
            ViewTaskCommand = new Command<int>(async (taskId) => await NavigateToTaskPage(taskId));
            LoadTasks();
        }

        private async void LoadTasks()
        {
            var tasks = await _dbService.GetTasks();
            foreach (var task in tasks)
            {
                TaskList.Add(task);
            }
        }

        private async Task NavigateToTaskPage(int taskId)
        {
            await Shell.Current.GoToAsync($"///TaskPage?taskId={taskId}");
        }
    }
}
