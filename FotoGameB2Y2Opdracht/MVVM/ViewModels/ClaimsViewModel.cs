using FotoGameB2Y2Opdracht.MVVM.Models;
using FotoGameB2Y2Opdracht.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace FotoGameB2Y2Opdracht.MVVM.ViewModels;

public class ClaimsViewModel : BaseViewModel
{
    private readonly LocalDbService _dbService;
    private readonly UserService _userService;

    public ICommand ViewDetailsCommand { get; }

    public ObservableCollection<Claim> Claims { get; } = new();

    public ClaimsViewModel(LocalDbService dbService, UserService userService)
    {
        _dbService = dbService;
        _userService = userService;

        ViewDetailsCommand = new Command<int>(async (claimId) => await NavigateToClaimDetails(claimId));

        LoadClaims();
    }

    private async Task NavigateToClaimDetails(int claimId)
    {
        await Shell.Current.GoToAsync($"///ClaimDetailsPage?claimId={claimId}");
    }

    private async void LoadClaims()
    {
        var allClaims = await _dbService.GetAllClaims();

        var currentUser = _userService.GetCurrentUser();
        if (currentUser != null)
        {
            var userClaims = allClaims.Where(c => c.UserId == currentUser.Id);

            Claims.Clear();
            foreach (var claim in userClaims)
            {
                Claims.Add(claim);
            }
        }
    }
}
