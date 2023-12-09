using AudioNav.ViewModels;
using ReactiveUI;
using ReactiveUI.Maui;
using System.Reactive.Disposables;

namespace AudioNav.Views;

public partial class SimpleHeadingView : ReactiveContentView<SimpleHeadingViewModel>
{
    public SimpleHeadingView()
    {
        InitializeComponent();
        this.WhenActivated(disposables =>
        {
            this.OneWayBind(ViewModel, vm => vm.HeadingText, v => v.HeadingLabel.Text).DisposeWith(disposables);
        });
    }
}