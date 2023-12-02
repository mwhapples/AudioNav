using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Controls;

namespace AudioNav.Views;

public partial class AboutPage : ContentPage
{
    public string AboutVersionInfo => $"{AppInfo.Current.Name} {AppInfo.Current.VersionString}";
    public AboutPage()
    {
        InitializeComponent();
        BindingContext = this;
    }
}