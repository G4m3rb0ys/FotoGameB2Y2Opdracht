using FotoGameB2Y2Opdracht.MVVM.ViewModels;
using FotoGameB2Y2Opdracht.MVVM.Models;

namespace FotoGameB2Y2Opdracht.MVVM.Views;

public partial class ClaimsPage : ContentPage
{
    public ClaimsPage(ClaimsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    private async void OnViewDetailsClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var claim = button?.BindingContext as Claim;

        if (claim != null)
        {
            await Shell.Current.GoToAsync($"ClaimDetailsPage?claimId={claim.Id}");

        }
    }


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
