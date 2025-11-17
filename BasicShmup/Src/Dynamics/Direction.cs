using BasicShmup.Inputs;
using Godot;

namespace BasicShmup.Dynamics;

public readonly record struct Direction()
{
    private const float Tolerance = FloatConstants.Tolerance;

    public static readonly Direction Right = new(Vector2.Right);
    public static readonly Direction Left = new(Vector2.Left);

    public Vector2 VectorValue { get; }

    public Direction(Vector2 direction) : this()
    {
        var directionLength = direction.Length();

        VectorValue = directionLength switch
        {
            < Tolerance => Vector2.Zero,
            <= 1 => direction,
            _ => direction / directionLength
        };
    }
}
