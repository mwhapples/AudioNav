using AudioNav.Models;
using ReactiveUI;
using System.Reactive.Disposables;
using System.Reactive.Subjects;

namespace AudioNav.ViewModels;

public class FilterRateViewModel : ReactiveObject, IActivatableViewModel
{
    private readonly IFilteredSensor filteredSensor;
    private readonly ObservableAsPropertyHelper<double> compassFilterRate;
    public FilterRateViewModel(IFilteredSensor filteredSensor)
    {
        this.filteredSensor = filteredSensor;
        compassFilterRate = filteredSensor.FilterRate.ToProperty(this, x => x.CompassFilterRate);
    }
    public ViewModelActivator Activator { get; } = new();
    public double CompassFilterRate
    {
        get => compassFilterRate.Value;
        set => filteredSensor.ChangeFilterRate(value);
    }
}
