using AudioNav.Utils;
using System;
using System.Collections.Immutable;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace AudioNav.Models;

public class AudioCompass
{
    private readonly ISubject<ICompassProvider> compassProvider;
    private readonly ISubject<Heading> course;
    public AudioCompass()
    {
        course = new BehaviorSubject<Heading>(Heading.FromDegrees(0));
        AvailableSensors = [ new MagneticCompassProvider(), new DummyCompassProvider() ];
        compassProvider = new BehaviorSubject<ICompassProvider>(AvailableSensors.First());
    }
    public ImmutableList<ICompassProvider> AvailableSensors { get; }
    public IObservable<ICompassProvider> CompassProvider => compassProvider.AsObservable();
    public void ChangeCompassProvider(ICompassProvider newCompassProvider) => compassProvider.OnNext(newCompassProvider);
    public IObservable<Heading> Course => course.AsObservable();
    public void ChangeCourse(Heading newCourse) => course.OnNext(newCourse);
    public IObservable<CompassData> CompassHeading => compassProvider.Select(x => x.CompassObservable).Switch();
}
