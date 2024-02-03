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
        ImmutableList<ICompassProvider> sensors = [new MagneticCompassProvider(), new DummyCompassProvider()];
        AvailableSensors = sensors.Where(x => x.IsSupported).ToImmutableList();
        AvailableOutputs = [new DummyOutput(), new SpeakAbsoluteHeadingOutput(), new SpeakRelativeHeadingOutput()];
        course = new BehaviorSubject<Heading>(Heading.FromDegrees(0));
        compassProvider = new BehaviorSubject<ICompassProvider>(AvailableSensors.First());
        output = new BehaviorSubject<IAudioCompassOutput>(AvailableOutputs.First());
        outputData = new AudioCompassOutputData(CompassHeading, Course);
        outputDisposable = output.Select(x => x.CreateOutputObservable(outputData)).Switch().Subscribe();
    }
    public ImmutableList<ICompassProvider> AvailableSensors { get; }
    public ImmutableList<IAudioCompassOutput> AvailableOutputs { get; }
    public IObservable<ICompassProvider> CompassProvider => compassProvider.AsObservable();
    public void ChangeCompassProvider(ICompassProvider newCompassProvider) => compassProvider.OnNext(newCompassProvider);
    public IObservable<Heading> Course => course.AsObservable();
    public void ChangeCourse(Heading newCourse) => course.OnNext(newCourse);
    public IObservable<CompassData> CompassHeading => compassProvider.Select(x => x.CompassObservable).Switch();
    public IObservable<IAudioCompassOutput> Output => output.AsObservable();
    public void ChangeOutput(IAudioCompassOutput value) => output.OnNext(value);
    public void Dispose() => outputDisposable.Dispose();
}
