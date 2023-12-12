using AudioNav.ViewModels;
using ReactiveUI;
using ReactiveUI.Maui;

namespace AudioNav.Views;

public partial class SpeakAbsoluteHeadingView : ReactiveContentView<SpeakAbsoluteHeadingViewModel>
{
    public SpeakAbsoluteHeadingView()
    {
        InitializeComponent();
        this.WhenActivated(disposables => { });
    }
}