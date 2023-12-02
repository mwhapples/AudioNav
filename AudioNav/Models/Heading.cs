using AudioNav.Utils;
using System;
using System.Numerics;

namespace AudioNav.Models;

public record Heading
{
    private Heading(double degrees)
    {
        Degrees = degrees;
    }
    public double Degrees { get; init; }
    public Complex ToComplex() => Complex.FromPolarCoordinates(1.0, Degrees.ToRadians());
    public static Heading FromDegrees(double degrees)
    {
        var d = degrees % 360.0;
        return new Heading(d < 0.0 ? d + 360.0 : d);
    }
    public static Heading FromComplex(Complex complex) => Heading.FromDegrees(complex.Phase.ToDegrees());
    public static Heading operator +(Heading a, Heading b) => Heading.FromDegrees(a.Degrees + b.Degrees);
    public static Heading operator -(Heading a, Heading b) => Heading.FromDegrees(a.Degrees - b.Degrees);
}
