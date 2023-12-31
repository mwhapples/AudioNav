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
            this.OneWayBind(ViewModel, vm => vm.FilteredSensor, v => v.CompassFilterRateSlider.IsEnabled, x => x is not null).DisposeWith(disposables);
            this.Bind(ViewModel, vm => vm.CompassFilterRate, v => v.CompassFilterRateSlider.Value).DisposeWith(disposables);
        });
    }
}