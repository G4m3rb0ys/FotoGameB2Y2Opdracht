using System;
using Microsoft.Maui;
using Microsoft.Maui.Hosting;

namespace FotoGameB2Y2Opdracht
{
    internal class Program : MauiApplication
    {
        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

        static void Main(string[] args)
        {
            var app = new Program();
            app.Run(args);
        }
    }
}
