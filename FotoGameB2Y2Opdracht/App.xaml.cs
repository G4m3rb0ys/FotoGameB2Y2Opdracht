using FotoGameB2Y2Opdracht.Services;

namespace FotoGameB2Y2Opdracht
{
    public partial class App : Application
    {
        public App(LocalDbService dbService)
        {
            InitializeComponent();

            Task.Run(async () => await dbService.InitializeDatabase());

            MainPage = new AppShell();
        }
    }
}