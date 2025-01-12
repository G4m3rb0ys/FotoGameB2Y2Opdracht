using FotoGameB2Y2Opdracht.MVVM.Models;
using FotoGameB2Y2Opdracht.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace FotoGameB2Y2Opdracht.MVVM.ViewModels;

public class TasksViewModel : BaseViewModel
{
    private readonly LocalDbService _dbService;
    public ObservableCollection<Tasks> TaskList { get; } = new();

    public ICommand DeleteTaskCommand { get; }

    private bool _hasTasks;
    public bool HasTasks
    {
        get => _hasTasks;
        set => SetProperty(ref _hasTasks, value);
    }

    public TasksViewModel(LocalDbService dbService)
    {
        _dbService = dbService;

        DeleteTaskCommand = new Command<int>(async (taskId) => await DeleteTask(taskId));

        LoadTasks();
    }

    private async void LoadTasks()
    {
        var tasks = await _dbService.GetTasks();
        TaskList.Clear();
        foreach (var task in tasks)
        {
            TaskList.Add(task);
        }
        UpdateUI();
    }

    private async Task DeleteTask(int taskId)
    {
        var taskToDelete = TaskList.FirstOrDefault(t => t.Id == taskId);
        if (taskToDelete != null)
        {
            await _dbService.DeleteTask(taskToDelete);
            TaskList.Remove(taskToDelete);
        }
        UpdateUI();
    }

    public void UpdateUI()
    {
        HasTasks = TaskList.Any();
    }

    public async Task AddTask(Tasks task)
    {
        await _dbService.CreateTask(task);
        TaskList.Add(task);
        UpdateUI();
    }

    public async Task LoadTasksAsync()
    {
        var tasks = await _dbService.GetTasks();
        TaskList.Clear();
        foreach (var task in tasks.OrderBy(t => t.Deadline))
        {
            TaskList.Add(task);
        }
        UpdateUI();
    }

}
