using AudioNav.Models;
using ReactiveUI;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace AudioNav.ViewModels;

public class FilterRateViewModel : ReactiveObject, IActivatableViewModel
{
    private readonly ObservableAsPropertyHelper<IFilteredSensor?> filteredSensor;
    private readonly ObservableAsPropertyHelper<double> compassFilterRate;
    public FilterRateViewModel(AudioCompass audioCompass)
    {
        filteredSensor = audioCompass.CompassProvider.Select(x => x is IFilteredSensor ? (IFilteredSensor)x : null).ToProperty(this, x => x.FilteredSensor);
        compassFilterRate = this.WhenAnyValue(x => x.FilteredSensor).WhereNotNull().Select(x => x.FilterRate).Switch().ToProperty(this, x => x.CompassFilterRate);
    }
    public ViewModelActivator Activator { get; } = new();
    public IFilteredSensor? FilteredSensor => filteredSensor.Value;
    public double CompassFilterRate
    {
        get => compassFilterRate.Value;
        set => FilteredSensor?.ChangeFilterRate(value);
    }
}
