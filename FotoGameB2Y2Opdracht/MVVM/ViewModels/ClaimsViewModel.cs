using FotoGameB2Y2Opdracht.MVVM.Models;
using FotoGameB2Y2Opdracht.Services;
using System.Collections.ObjectModel;

namespace FotoGameB2Y2Opdracht.MVVM.ViewModels;

public class ClaimsViewModel : BaseViewModel
{
    private readonly LocalDbService _dbService;
    private readonly UserService _userService;

    public ObservableCollection<Claim> Claims { get; } = new();

    public ClaimsViewModel(LocalDbService dbService, UserService userService)
    {
        _dbService = dbService;
        _userService = userService;

        LoadClaims();
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
