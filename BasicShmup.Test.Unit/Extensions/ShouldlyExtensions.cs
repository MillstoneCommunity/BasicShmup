using BasicShmup.Domain;
using BasicShmup.Domain.Dynamics;
using Shouldly;

namespace BasicShmup.Test.Unit.Extensions;

public static class ShouldlyExtensions
{
    private const float Tolerance = FloatConstants.Tolerance + 0.0001f;

    public static void ShouldBeCloseTo(this Position actual, Position expected)
    {
        actual.AssertAwesomely(
            position => IsCloseTo(position.X, expected.X) && IsCloseTo(position.Y, expected.Y),
            actual,
            expected);
    }

    public static void ShouldNotBeCloseTo(this Position actual, Position expected)
    {
        actual.AssertAwesomely(
            position => !IsCloseTo(position.X, expected.X) || !IsCloseTo(position.Y, expected.Y),
            actual,
            expected);
    }

    private static bool IsCloseTo(float actual, float expected)
    {
        return expected - Tolerance < actual && actual < expected + Tolerance;
    }
}
