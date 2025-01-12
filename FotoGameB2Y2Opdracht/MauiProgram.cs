using FotoGameB2Y2Opdracht.MVVM.Models;
using FotoGameB2Y2Opdracht.MVVM.Views;
using FotoGameB2Y2Opdracht.MVVM.ViewModels;
using FotoGameB2Y2Opdracht.Services;
using Microsoft.Extensions.Logging;
using SQLite;

namespace FotoGameB2Y2Opdracht
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "FotoGameB2Y2Opdracht.db3");
            builder.Services.AddSingleton(s => new LocalDbService(dbPath));
            builder.Services.AddSingleton<UserService>();

            builder.Services.AddSingleton<TasksViewModel>();
            builder.Services.AddSingleton<ClaimsViewModel>();




#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
