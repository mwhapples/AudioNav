using AudioNav.Utils;
using System;
using System.Collections.Immutable;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;

namespace AudioNav.Models;

public class AudioCompass : IDisposable
{
    private readonly ISubject<ICompassProvider> compassProvider;
    private readonly ISubject<Heading> course;
    private readonly ISubject<IAudioCompassOutput> output;
    private IDisposable outputDisposable;
    private readonly AudioCompassOutputData outputData;
    public AudioCompass()
    {
        course = new BehaviorSubject<Heading>(Heading.FromDegrees(0));
        AvailableSensors = [ new MagneticCompassProvider(), new DummyCompassProvider() ];
        compassProvider = new BehaviorSubject<ICompassProvider>(AvailableSensors.First());
        outputData = new AudioCompassOutputData(CompassHeading, Course);
        output = new BehaviorSubject<IAudioCompassOutput>(new SpeakAbsoluteHeadingOutput());
        outputDisposable = output.Select(x => Observable.Create<Unit>(async (observer, cancellationToken) => await x.RunAsync(outputData, cancellationToken))).Switch().Subscribe();
    }
    public ImmutableList<ICompassProvider> AvailableSensors { get; }
    public IObservable<ICompassProvider> CompassProvider => compassProvider.AsObservable();
    public void ChangeCompassProvider(ICompassProvider newCompassProvider) => compassProvider.OnNext(newCompassProvider);
    public IObservable<Heading> Course => course.AsObservable();
    public void ChangeCourse(Heading newCourse) => course.OnNext(newCourse);
    public IObservable<CompassData> CompassHeading => compassProvider.Select(x => x.CompassObservable).Switch();
    public IObservable<IAudioCompassOutput> Output => output.AsObservable();
    public void ChangeOutput(IAudioCompassOutput value) => output.OnNext(value);
    public void Dispose() => outputDisposable.Dispose();
}
