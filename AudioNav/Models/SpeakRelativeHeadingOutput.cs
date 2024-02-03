using Microsoft.Maui.Media;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Reactive;
using System.Threading.Tasks;
using System.Threading;
using System;
using AudioNav.Utils;

namespace AudioNav.Models;

internal class SpeakRelativeHeadingOutput : IAudioCompassOutput
{
    public string Name { get; } = "Speak relative heading";
    public async Task RunAsync(AudioCompassOutputData outputData, CancellationToken cancellationToken)
    {
        var heading = new BehaviorSubject<CompassData>(new CompassData.NoHeading());
        var course = new BehaviorSubject<Heading>(Heading.FromDegrees(0));
        using (outputData.SensorData.Subscribe(heading))
            using(outputData.Course.Subscribe(course))
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    (string text, SpeechOptions options) = heading.Value switch
                    {
                        CompassData.HeadingReading r => GenerateHeadingText(course.Value, r.Heading),
                        _ => ("No heading", new SpeechOptions())
                    };
                    await TextToSpeech.Default.SpeakAsync(text, options: options, cancelToken: cancellationToken);
                    await Task.Delay(TimeSpan.FromMilliseconds(500), cancellationToken: cancellationToken);
                }
                catch (Exception) { }
            }
        }
    }

    private (string, SpeechOptions) GenerateHeadingText(Heading course, Heading heading)
    {
        int relativeHeading = (int)Math.Round((heading - course).ToComplex().Phase.ToDegrees(), 0, MidpointRounding.AwayFromZero);
        SpeechOptions options = new SpeechOptions { Pitch = (float)(1.0 + (Math.Sign(relativeHeading) * 0.5)) };
        string text = relativeHeading switch
        {
            < 0 => "Minus",
            > 0 => "Plus",
            _ => ""
        };
        return (Math.Abs(relativeHeading).ToString($"{text} 0 0 0"), options);
    }

    public IObservable<Unit> CreateOutputObservable(AudioCompassOutputData outputData) => Observable.Create<Unit>(async (_, ct) => await RunAsync(outputData, ct));
}
