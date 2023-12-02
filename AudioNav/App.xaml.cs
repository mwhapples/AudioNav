using Microsoft.Maui.Controls;

namespace AudioNav;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        MainPage = new AppShell();
    }
}
