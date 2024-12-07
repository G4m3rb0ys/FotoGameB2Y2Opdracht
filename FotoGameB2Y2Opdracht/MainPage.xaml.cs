using Microsoft.Maui.Controls;

namespace FotoGameB2Y2Opdracht
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnProfileTapped(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("ProfilePage");
        }

        private async void OnShopTapped(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("ShopPage");
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
}
