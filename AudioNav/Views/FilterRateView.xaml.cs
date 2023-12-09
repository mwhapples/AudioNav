using AudioNav.ViewModels;
using ReactiveUI;
using ReactiveUI.Maui;
using System.Reactive.Disposables;

namespace AudioNav.Views;

public partial class FilterRateView : ReactiveContentView<FilterRateViewModel>
{
    public FilterRateView()
    {
        InitializeComponent();
        this.WhenActivated(disposables =>
        {
            this.Bind(ViewModel, vm => vm.CompassFilterRate, v => v.CompassFilterRateSlider.Value).DisposeWith(disposables);
        });
    }
}