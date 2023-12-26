using Microsoft.Maui.Media;
using System;
using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Tasks;

namespace AudioNav.Models;

public class SpeakAbsoluteHeadingOutput : IAudioCompassOutput
{
    public async Task RunAsync(AudioCompassOutputData outputData, CancellationToken cancellationToken)
    {
        var heading = new BehaviorSubject<CompassData>(new CompassData.NoHeading());
        using (outputData.SensorData.Subscribe(heading))
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    await TextToSpeech.Default.SpeakAsync(heading.Value switch
                    {
                        CompassData.HeadingReading r => r.Heading.Degrees.ToString("0 0 0"),
                        _ => "No heading"
                    }, cancelToken: cancellationToken);
                    await Task.Delay(TimeSpan.FromMilliseconds(500), cancellationToken: cancellationToken);
                }
                catch (Exception) { }
            }
        }
    }
}
