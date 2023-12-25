using AudioNav.Models;
using ReactiveUI;
using System.Reactive.Disposables;
using System.Reactive.Subjects;

namespace AudioNav.ViewModels;

public class FilterRateViewModel : ReactiveObject, IActivatableViewModel
{
    private IFilteredSensor filteredSensor;
    public FilterRateViewModel(IFilteredSensor filteredSensor)
    {
        this.filteredSensor = filteredSensor;
        compassFilterRate = filteredSensor.FilterRate.ToProperty(this, x => x.CompassFilterRate);
        this.WhenActivated(disposables =>
        {
            compassFilterRate.DisposeWith(disposables);
        });
    }
    public ViewModelActivator Activator { get; } = new();
    private readonly ObservableAsPropertyHelper<double> compassFilterRate;
    public double CompassFilterRate
    {
        get => compassFilterRate.Value;
        set => filteredSensor.ChangeFilterRate(value);
    }
}
