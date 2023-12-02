using System;

namespace AudioNav.Utils;

public static class DoubleExtensions
{
    private static  readonly double degreesToRadiansRatio = Math.PI / 180.0;
    public static double ToDegrees(this double value) => value / degreesToRadiansRatio;
    public static double ToRadians(this double value) => value * degreesToRadiansRatio;
}
