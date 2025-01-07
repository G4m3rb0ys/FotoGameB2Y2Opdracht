namespace FotoGameB2Y2Opdracht.MVVM.Views;

public partial class ClaimsPage : ContentPage
{
    public ClaimsPage()
    {
        InitializeComponent();

        // Hardcoded claims
        var claims = new List<Claim>
        {
            new Claim { Title = "Claimed Task 1", Description = "Description of claimed task 1.", Deadline = new DateTime(2023, 12, 20), Points = 50, IsWeekly = false },
            new Claim { Title = "Claimed Task 2", Description = "Description of claimed task 2.", Deadline = new DateTime(2023, 12, 25), Points = 30, IsWeekly = true }
        };

        ClaimsCollectionView.ItemsSource = claims;
    }

    private async void OnViewDetailsClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var claim = button?.BindingContext as Claim;

        if (claim != null)
        {
            var weeklyGoals = new List<string>
            {
                "Complete Week 1 goal",
                "Complete Week 2 goal",
                "Complete Week 3 goal"
            };

            string serializedGoals = string.Join(",", weeklyGoals);

            await Shell.Current.GoToAsync($"///ClaimDetailsPage?" +
                                           $"title={Uri.EscapeDataString(claim.Title)}&" +
                                           $"description={Uri.EscapeDataString(claim.Description)}&" +
                                           $"deadline={Uri.EscapeDataString(claim.Deadline.ToString("yyyy-MM-dd"))}&" +
                                           $"points={claim.Points}&" +
                                           $"isWeekly={claim.IsWeekly}&" +
                                           $"weeklyGoals={Uri.EscapeDataString(serializedGoals)}&" +
                                           $"currentWeek=2");
        }
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

public class Claim
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime Deadline { get; set; }
    public int Points { get; set; }
    public bool IsWeekly { get; set; }
}
