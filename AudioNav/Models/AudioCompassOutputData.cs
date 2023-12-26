using System;

namespace AudioNav.Models;

public record class AudioCompassOutputData(IObservable<CompassData> SensorData, IObservable<Heading> Course) { }
