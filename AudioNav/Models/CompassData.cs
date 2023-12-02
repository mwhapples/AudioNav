namespace AudioNav.Models;

public abstract class CompassData
{
    private CompassData() {}
    public sealed class NoHeading : CompassData {}
    public sealed class HeadingReading(Heading heading) : CompassData
    {
        public Heading Heading { get; } = heading;
    }
}
