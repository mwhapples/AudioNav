using AudioNav.Utils;
using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace AudioNav.Models;

internal class Audio_compass
{
    private readonly ISubject<ICompassProvider> compassProvider = new BehaviorSubject<ICompassProvider>(new MagneticCompassProvider());
    private readonly ISubject<Heading> course = new BehaviorSubject<Heading>(Heading.FromDegrees(0));
    private readonly ISubject<int> filterRate = new BehaviorSubject<int>(10);
    public IObservable<ICompassProvider> CompassProvider => compassProvider.AsObservable();
    public void ChangeCompassProvider(ICompassProvider newCompassProvider) => compassProvider.OnNext(newCompassProvider);
    public IObservable<Heading> Course => course.AsObservable();
    public void ChangeCourse(Heading newCourse) => course.OnNext(newCourse);
    public IObservable<CompassData> CompassHeading => compassProvider.Select(x => x.CompassObservable).Switch().LowPassFilter(filterRate);
    public IObservable<int> FilterRate => filterRate.AsObservable();
    public void ChangeFilterRate(int newFilterRate) => filterRate.OnNext(newFilterRate);
}
