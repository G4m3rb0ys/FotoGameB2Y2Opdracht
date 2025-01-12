using FotoGameB2Y2Opdracht.MVVM.Models;
using FotoGameB2Y2Opdracht.Services;
using System.Collections.ObjectModel;

namespace FotoGameB2Y2Opdracht.MVVM.ViewModels
{
    public class ClaimDetailsViewModel : BaseViewModel
    {
        private readonly LocalDbService _dbService;
        private readonly UserService _userService;

        public ObservableCollection<ClaimWeeklyGoal> WeeklyGoals { get; set; }

        public ClaimDetailsViewModel(LocalDbService dbService, UserService userService)
        {
            _dbService = dbService;
            _userService = userService;
            WeeklyGoals = new ObservableCollection<ClaimWeeklyGoal>();
        }

        public async Task LoadWeeklyGoals(int claimId)
        {
            var goals = await _dbService.GetWeeklyGoalsForClaim(claimId);
            WeeklyGoals.Clear();

            foreach (var goal in goals)
            {
                WeeklyGoals.Add(goal);
            }
        }
    }
}
