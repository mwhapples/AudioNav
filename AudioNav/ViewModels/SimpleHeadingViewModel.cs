﻿using AudioNav.Models;
using ReactiveUI;
using System.Reactive.Linq;

namespace AudioNav.ViewModels;

public class SimpleHeadingViewModel : ReactiveObject, IActivatableViewModel
{
    public SimpleHeadingViewModel(AudioCompass audioCompass)
    {
        headingText = audioCompass.CompassHeading.Select(x => x switch
        {
            CompassData.HeadingReading r => r.Heading.Degrees.ToString("000"),
            _ => "---"
        }).ToProperty(this, x => x.HeadingText);
    }
    public ViewModelActivator Activator { get; } = new();
    private readonly ObservableAsPropertyHelper<string> headingText;
    public string HeadingText => headingText.Value;
}
