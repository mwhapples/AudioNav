using AudioNav.Utils;
using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace AudioNav.Models;

public class AudioCompass
{
    private readonly ISubject<ICompassProvider> compassProvider = new BehaviorSubject<ICompassProvider>(new MagneticCompassProvider());
    private readonly ISubject<Heading> course = new BehaviorSubject<Heading>(Heading.FromDegrees(0));
    public IObservable<ICompassProvider> CompassProvider => compassProvider.AsObservable();
    public void ChangeCompassProvider(ICompassProvider newCompassProvider) => compassProvider.OnNext(newCompassProvider);
    public IObservable<Heading> Course => course.AsObservable();
    public void ChangeCourse(Heading newCourse) => course.OnNext(newCourse);
    public IObservable<CompassData> CompassHeading => compassProvider.Select(x => x.CompassObservable).Switch();
}
