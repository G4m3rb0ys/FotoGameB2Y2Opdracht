using Microsoft.Maui.Controls;
using System.Windows.Input;

namespace FotoGameB2Y2Opdracht;

public partial class AboutPage : ContentPage
{
    public ICommand OpenLinkCommand { get; }

    public AboutPage()
    {
        InitializeComponent();

        OpenLinkCommand = new Command<string>((url) =>
        {
            Browser.Default.OpenAsync(url, BrowserLaunchMode.SystemPreferred);
        });

        BindingContext = this;
    }
}
